using HeyBuns.Api.Data;
using HeyBuns.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HeyBuns.Api.Repositories;

public interface ISucursalRepository
{
    Task<List<Sucursal>> ListarAsync();
    Task<Sucursal?> ObtenerPorIdAsync(int id);
    Task CrearAsync(Sucursal sucursal);
    Task GuardarCambiosAsync();
}

public class SucursalRepository : ISucursalRepository
{
    private readonly AppDbContext _db;

    public SucursalRepository(AppDbContext db) => _db = db;

    public Task<List<Sucursal>> ListarAsync() =>
        _db.Sucursales.OrderBy(s => s.Nombre).ToListAsync();

    public Task<Sucursal?> ObtenerPorIdAsync(int id) =>
        _db.Sucursales.FirstOrDefaultAsync(s => s.Id == id);

    public async Task CrearAsync(Sucursal sucursal)
    {
        _db.Sucursales.Add(sucursal);
        await _db.SaveChangesAsync();
    }

    public Task GuardarCambiosAsync() => _db.SaveChangesAsync();
}
