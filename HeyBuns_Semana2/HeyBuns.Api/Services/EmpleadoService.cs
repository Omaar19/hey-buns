using HeyBuns.Api.DTOs;
using HeyBuns.Api.Exceptions;
using HeyBuns.Api.Models;
using HeyBuns.Api.Repositories;

namespace HeyBuns.Api.Services;

public interface IEmpleadoService
{
    Task<List<EmpleadoRespuestaDto>> ListarAsync(int? sucursalId);
    Task<EmpleadoRespuestaDto> CrearAsync(EmpleadoDto dto);
    Task<EmpleadoRespuestaDto> ActualizarAsync(int id, EmpleadoDto dto);
    Task EliminarAsync(int id, int idActual);
}

public class EmpleadoService : IEmpleadoService
{
    private readonly IUsuarioRepository _usuarios;
    private readonly ISucursalRepository _sucursales;

    public EmpleadoService(IUsuarioRepository usuarios, ISucursalRepository sucursales)
    {
        _usuarios = usuarios;
        _sucursales = sucursales;
    }

    public async Task<List<EmpleadoRespuestaDto>> ListarAsync(int? sucursalId)
    {
        var lista = await _usuarios.ListarAsync(sucursalId);
        return lista.Select(Mapear).ToList();
    }

    public async Task<EmpleadoRespuestaDto> CrearAsync(EmpleadoDto dto)
    {
        var correo = dto.Correo.Trim().ToLower();
        await ValidarAsync(dto, correo, null);

        if (string.IsNullOrWhiteSpace(dto.Password) || dto.Password.Length < 6)
            throw new ReglaNegocioException("La contraseña debe tener al menos 6 caracteres");

        var usuario = new Usuario
        {
            Nombre = dto.Nombre.Trim(),
            Correo = correo,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Rol = dto.Rol,
            SucursalId = dto.Rol == Roles.Admin ? null : dto.SucursalId,
            FechaRegistro = DateTime.Now
        };

        await _usuarios.CrearAsync(usuario);
        return await ObtenerAsync(usuario.Id);
    }

    public async Task<EmpleadoRespuestaDto> ActualizarAsync(int id, EmpleadoDto dto)
    {
        var usuario = await _usuarios.ObtenerPorIdAsync(id)
            ?? throw new NoEncontradoException("El empleado no existe");

        var correo = dto.Correo.Trim().ToLower();
        await ValidarAsync(dto, correo, id);

        usuario.Nombre = dto.Nombre.Trim();
        usuario.Correo = correo;
        usuario.Rol = dto.Rol;
        usuario.SucursalId = dto.Rol == Roles.Admin ? null : dto.SucursalId;

        if (!string.IsNullOrWhiteSpace(dto.Password))
        {
            if (dto.Password.Length < 6)
                throw new ReglaNegocioException("La contraseña debe tener al menos 6 caracteres");
            usuario.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        }

        await _usuarios.GuardarCambiosAsync();
        return await ObtenerAsync(id);
    }

    public async Task EliminarAsync(int id, int idActual)
    {
        if (id == idActual)
            throw new ReglaNegocioException("No puedes eliminar tu propia cuenta");

        var usuario = await _usuarios.ObtenerPorIdAsync(id)
            ?? throw new NoEncontradoException("El empleado no existe");

        await _usuarios.EliminarAsync(usuario);
    }

    private async Task<EmpleadoRespuestaDto> ObtenerAsync(int id)
    {
        var usuario = await _usuarios.ObtenerPorIdAsync(id)
            ?? throw new NoEncontradoException("El empleado no existe");
        return Mapear(usuario);
    }

    private async Task ValidarAsync(EmpleadoDto dto, string correo, int? excluirId)
    {
        if (!Roles.Todos.Contains(dto.Rol))
            throw new ReglaNegocioException("El rol debe ser Admin, Cocinero o Mesero");

        if (await _usuarios.ExisteCorreoAsync(correo, excluirId))
            throw new ReglaNegocioException("Ya existe un empleado con ese correo");

        if (dto.Rol != Roles.Admin)
        {
            if (!dto.SucursalId.HasValue)
                throw new ReglaNegocioException("El cocinero y el mesero necesitan una sucursal");
            if (!await _sucursales.ExisteAsync(dto.SucursalId.Value))
                throw new ReglaNegocioException("La sucursal no existe");
        }
    }

    private static EmpleadoRespuestaDto Mapear(Usuario u) =>
        new(u.Id, u.Nombre, u.Correo, u.Rol, u.SucursalId, u.Sucursal?.Nombre);
}
