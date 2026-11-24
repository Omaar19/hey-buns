using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HeyBuns.Api.DTOs;
using HeyBuns.Api.Exceptions;
using HeyBuns.Api.Models;
using HeyBuns.Api.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace HeyBuns.Api.Services;

public interface IAuthService
{
    Task<AuthRespuestaDto> RegistrarAsync(RegistroDto dto);
    Task<AuthRespuestaDto> LoginAsync(LoginDto dto);
}

public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _usuarios;
    private readonly IConfiguration _config;

    public AuthService(IUsuarioRepository usuarios, IConfiguration config)
    {
        _usuarios = usuarios;
        _config = config;
    }

    public async Task<AuthRespuestaDto> RegistrarAsync(RegistroDto dto)
    {
        var correo = dto.Correo.Trim().ToLower();

        if (await _usuarios.ExisteCorreoAsync(correo))
            throw new ReglaNegocioException("Ya existe una cuenta con ese correo");

        var usuario = new Usuario
        {
            Nombre = dto.Nombre.Trim(),
            Correo = correo,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Rol = "Cliente",
            FechaRegistro = DateTime.Now
        };

        await _usuarios.CrearAsync(usuario);
        return CrearRespuesta(usuario);
    }

    public async Task<AuthRespuestaDto> LoginAsync(LoginDto dto)
    {
        var correo = dto.Correo.Trim().ToLower();
        var usuario = await _usuarios.ObtenerPorCorreoAsync(correo);

        if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Password, usuario.PasswordHash))
            throw new CredencialesInvalidasException();

        return CrearRespuesta(usuario);
    }

    private AuthRespuestaDto CrearRespuesta(Usuario usuario) => new()
    {
        Token = GenerarToken(usuario),
        Id = usuario.Id,
        Nombre = usuario.Nombre,
        Correo = usuario.Correo,
        Rol = usuario.Rol
    };

    private string GenerarToken(Usuario usuario)
    {
        var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var credenciales = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.Nombre),
            new Claim(ClaimTypes.Email, usuario.Correo),
            new Claim(ClaimTypes.Role, usuario.Rol)
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:Minutos"]!)),
            signingCredentials: credenciales);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
