using HeyBuns.Api.Data;
using HeyBuns.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HeyBuns.Api.Repositories;

public interface ICategoriaRepository
{
    Task<List<Categoria>> ListarAsync();
    Task<bool> ExisteAsync(int id);
}

public class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext _db;

    public CategoriaRepository(AppDbContext db) => _db = db;

    public Task<List<Categoria>> ListarAsync() =>
        _db.Categorias.OrderBy(c => c.Nombre).ToListAsync();

    public Task<bool> ExisteAsync(int id) =>
        _db.Categorias.AnyAsync(c => c.Id == id);
}
