using HeyBuns.Api.Data;
using HeyBuns.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HeyBuns.Api.Repositories;

public interface ISucursalRepository
{
    Task<List<Sucursal>> ListarAsync();
    Task<bool> ExisteAsync(int id);
}

public class SucursalRepository : ISucursalRepository
{
    private readonly AppDbContext _db;

    public SucursalRepository(AppDbContext db) => _db = db;

    public Task<List<Sucursal>> ListarAsync() =>
        _db.Sucursales.Where(s => s.Activa).OrderBy(s => s.Nombre).ToListAsync();

    public Task<bool> ExisteAsync(int id) =>
        _db.Sucursales.AnyAsync(s => s.Id == id);
}
