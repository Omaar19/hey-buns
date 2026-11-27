using HeyBuns.Api.Models;
using HeyBuns.Api.Repositories;

namespace HeyBuns.Api.Services;

public interface ISucursalService
{
    Task<List<Sucursal>> ListarAsync();
}

public class SucursalService : ISucursalService
{
    private readonly ISucursalRepository _sucursales;

    public SucursalService(ISucursalRepository sucursales) => _sucursales = sucursales;

    public Task<List<Sucursal>> ListarAsync() => _sucursales.ListarAsync();
}
