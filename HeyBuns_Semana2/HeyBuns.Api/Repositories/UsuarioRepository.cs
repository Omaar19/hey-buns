using HeyBuns.Api.Data;
using HeyBuns.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HeyBuns.Api.Repositories;

public interface IUsuarioRepository
{
    Task<Usuario?> ObtenerPorCorreoAsync(string correo);
    Task<Usuario?> ObtenerPorIdAsync(int id);
    Task<List<Usuario>> ListarAsync(int? sucursalId);
    Task<bool> ExisteCorreoAsync(string correo, int? excluirId);
    Task CrearAsync(Usuario usuario);
    Task GuardarCambiosAsync();
    Task EliminarAsync(Usuario usuario);
}

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _db;

    public UsuarioRepository(AppDbContext db) => _db = db;

    public Task<Usuario?> ObtenerPorCorreoAsync(string correo) =>
        _db.Usuarios.Include(u => u.Sucursal).FirstOrDefaultAsync(u => u.Correo == correo);

    public Task<Usuario?> ObtenerPorIdAsync(int id) =>
        _db.Usuarios.Include(u => u.Sucursal).FirstOrDefaultAsync(u => u.Id == id);

    public async Task<List<Usuario>> ListarAsync(int? sucursalId)
    {
        var consulta = _db.Usuarios.Include(u => u.Sucursal).AsQueryable();
        if (sucursalId.HasValue)
            consulta = consulta.Where(u => u.SucursalId == sucursalId.Value);
        return await consulta.OrderBy(u => u.Nombre).ToListAsync();
    }

    public Task<bool> ExisteCorreoAsync(string correo, int? excluirId) =>
        _db.Usuarios.AnyAsync(u => u.Correo == correo && (excluirId == null || u.Id != excluirId));

    public async Task CrearAsync(Usuario usuario)
    {
        _db.Usuarios.Add(usuario);
        await _db.SaveChangesAsync();
    }

    public Task GuardarCambiosAsync() => _db.SaveChangesAsync();

    public async Task EliminarAsync(Usuario usuario)
    {
        _db.Usuarios.Remove(usuario);
        await _db.SaveChangesAsync();
    }
}
