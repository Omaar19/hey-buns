using HeyBuns.Api.Models;
using HeyBuns.Api.Repositories;

namespace HeyBuns.Api.Services;

public interface ICategoriaService
{
    Task<List<Categoria>> ListarAsync();
}

public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaRepository _categorias;

    public CategoriaService(ICategoriaRepository categorias) => _categorias = categorias;

    public Task<List<Categoria>> ListarAsync() => _categorias.ListarAsync();
}
