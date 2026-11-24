using HeyBuns.Api.Data;
using HeyBuns.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HeyBuns.Api.Repositories;

public interface IProductoRepository
{
    Task<List<Producto>> ListarAsync(int? categoriaId);
    Task<Producto?> ObtenerPorIdAsync(int id);
    Task CrearAsync(Producto producto);
    Task GuardarCambiosAsync();
    Task EliminarAsync(Producto producto);
}

public class ProductoRepository : IProductoRepository
{
    private readonly AppDbContext _db;

    public ProductoRepository(AppDbContext db) => _db = db;

    public async Task<List<Producto>> ListarAsync(int? categoriaId)
    {
        var consulta = _db.Productos.Include(p => p.Categoria).AsQueryable();
        if (categoriaId.HasValue)
            consulta = consulta.Where(p => p.CategoriaId == categoriaId.Value);
        return await consulta.OrderBy(p => p.Nombre).ToListAsync();
    }

    public Task<Producto?> ObtenerPorIdAsync(int id) =>
        _db.Productos.Include(p => p.Categoria).FirstOrDefaultAsync(p => p.Id == id);

    public async Task CrearAsync(Producto producto)
    {
        _db.Productos.Add(producto);
        await _db.SaveChangesAsync();
    }

    public Task GuardarCambiosAsync() => _db.SaveChangesAsync();

    public async Task EliminarAsync(Producto producto)
    {
        _db.Productos.Remove(producto);
        await _db.SaveChangesAsync();
    }
}
