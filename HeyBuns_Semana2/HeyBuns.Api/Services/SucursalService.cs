using HeyBuns.Api.DTOs;
using HeyBuns.Api.Exceptions;
using HeyBuns.Api.Models;
using HeyBuns.Api.Repositories;

namespace HeyBuns.Api.Services;

public interface ISucursalService
{
    Task<List<Sucursal>> ListarAsync();
    Task<Sucursal> CrearAsync(SucursalDto dto);
    Task<Sucursal> ActualizarAsync(int id, SucursalDto dto);
}

public class SucursalService : ISucursalService
{
    private readonly ISucursalRepository _sucursales;

    public SucursalService(ISucursalRepository sucursales) => _sucursales = sucursales;

    public Task<List<Sucursal>> ListarAsync() => _sucursales.ListarAsync();

    public async Task<Sucursal> CrearAsync(SucursalDto dto)
    {
        var sucursal = new Sucursal
        {
            Nombre = dto.Nombre.Trim(),
            Direccion = dto.Direccion.Trim(),
            Telefono = dto.Telefono?.Trim(),
            Horario = dto.Horario.Trim(),
            Activa = dto.Activa
        };

        await _sucursales.CrearAsync(sucursal);
        return sucursal;
    }

    public async Task<Sucursal> ActualizarAsync(int id, SucursalDto dto)
    {
        var sucursal = await _sucursales.ObtenerPorIdAsync(id)
            ?? throw new NoEncontradoException("La sucursal no existe");

        sucursal.Nombre = dto.Nombre.Trim();
        sucursal.Direccion = dto.Direccion.Trim();
        sucursal.Telefono = dto.Telefono?.Trim();
        sucursal.Horario = dto.Horario.Trim();
        sucursal.Activa = dto.Activa;

        await _sucursales.GuardarCambiosAsync();
        return sucursal;
    }
}
