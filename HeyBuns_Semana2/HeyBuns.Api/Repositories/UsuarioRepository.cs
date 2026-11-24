using HeyBuns.Api.Data;
using HeyBuns.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HeyBuns.Api.Repositories;

public interface IUsuarioRepository
{
    Task<Usuario?> ObtenerPorCorreoAsync(string correo);
    Task<bool> ExisteCorreoAsync(string correo);
    Task CrearAsync(Usuario usuario);
}

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _db;

    public UsuarioRepository(AppDbContext db) => _db = db;

    public Task<Usuario?> ObtenerPorCorreoAsync(string correo) =>
        _db.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo);

    public Task<bool> ExisteCorreoAsync(string correo) =>
        _db.Usuarios.AnyAsync(u => u.Correo == correo);

    public async Task CrearAsync(Usuario usuario)
    {
        _db.Usuarios.Add(usuario);
        await _db.SaveChangesAsync();
    }
}
