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

    public async Task<AuthRespuestaDto> LoginAsync(LoginDto dto)
    {
        var correo = dto.Correo.Trim().ToLower();
        var usuario = await _usuarios.ObtenerPorCorreoAsync(correo);

        if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Password, usuario.PasswordHash))
            throw new CredencialesInvalidasException();

        return new AuthRespuestaDto
        {
            Token = GenerarToken(usuario),
            Id = usuario.Id,
            Nombre = usuario.Nombre,
            Correo = usuario.Correo,
            Rol = usuario.Rol,
            SucursalId = usuario.SucursalId,
            SucursalNombre = usuario.Sucursal?.Nombre
        };
    }

    private string GenerarToken(Usuario usuario)
    {
        var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var credenciales = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.Nombre),
            new Claim(ClaimTypes.Email, usuario.Correo),
            new Claim(ClaimTypes.Role, usuario.Rol)
        };

        // El cocinero y el mesero llevan su sucursal dentro del token
        if (usuario.SucursalId.HasValue)
            claims.Add(new Claim("SucursalId", usuario.SucursalId.Value.ToString()));

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:Minutos"]!)),
            signingCredentials: credenciales);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
