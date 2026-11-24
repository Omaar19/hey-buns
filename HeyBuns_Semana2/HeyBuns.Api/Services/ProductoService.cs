using HeyBuns.Api.DTOs;
using HeyBuns.Api.Exceptions;
using HeyBuns.Api.Models;
using HeyBuns.Api.Repositories;

namespace HeyBuns.Api.Services;

public interface IProductoService
{
    Task<List<ProductoRespuestaDto>> ListarAsync(int? categoriaId);
    Task<ProductoRespuestaDto> ObtenerAsync(int id);
    Task<ProductoRespuestaDto> CrearAsync(ProductoDto dto);
    Task<ProductoRespuestaDto> ActualizarAsync(int id, ProductoDto dto);
    Task EliminarAsync(int id);
}

public class ProductoService : IProductoService
{
    private readonly IProductoRepository _productos;
    private readonly ICategoriaRepository _categorias;

    public ProductoService(IProductoRepository productos, ICategoriaRepository categorias)
    {
        _productos = productos;
        _categorias = categorias;
    }

    public async Task<List<ProductoRespuestaDto>> ListarAsync(int? categoriaId)
    {
        var lista = await _productos.ListarAsync(categoriaId);
        return lista.Select(Mapear).ToList();
    }

    public async Task<ProductoRespuestaDto> ObtenerAsync(int id)
    {
        var producto = await _productos.ObtenerPorIdAsync(id)
            ?? throw new NoEncontradoException("El producto no existe");
        return Mapear(producto);
    }

    public async Task<ProductoRespuestaDto> CrearAsync(ProductoDto dto)
    {
        await ValidarCategoriaAsync(dto.CategoriaId);

        var producto = new Producto
        {
            CategoriaId = dto.CategoriaId,
            Nombre = dto.Nombre.Trim(),
            Descripcion = dto.Descripcion?.Trim(),
            Precio = dto.Precio,
            ImagenUrl = dto.ImagenUrl?.Trim(),
            Disponible = dto.Disponible
        };

        await _productos.CrearAsync(producto);
        return await ObtenerAsync(producto.Id);
    }

    public async Task<ProductoRespuestaDto> ActualizarAsync(int id, ProductoDto dto)
    {
        var producto = await _productos.ObtenerPorIdAsync(id)
            ?? throw new NoEncontradoException("El producto no existe");

        await ValidarCategoriaAsync(dto.CategoriaId);

        producto.CategoriaId = dto.CategoriaId;
        producto.Nombre = dto.Nombre.Trim();
        producto.Descripcion = dto.Descripcion?.Trim();
        producto.Precio = dto.Precio;
        producto.ImagenUrl = dto.ImagenUrl?.Trim();
        producto.Disponible = dto.Disponible;

        await _productos.GuardarCambiosAsync();
        return await ObtenerAsync(id);
    }

    public async Task EliminarAsync(int id)
    {
        var producto = await _productos.ObtenerPorIdAsync(id)
            ?? throw new NoEncontradoException("El producto no existe");
        await _productos.EliminarAsync(producto);
    }

    private async Task ValidarCategoriaAsync(int categoriaId)
    {
        if (!await _categorias.ExisteAsync(categoriaId))
            throw new ReglaNegocioException("La categoría no existe");
    }

    private static ProductoRespuestaDto Mapear(Producto p) => new(
        p.Id, p.CategoriaId, p.Categoria.Nombre, p.Nombre, p.Descripcion, p.Precio, p.ImagenUrl, p.Disponible);
}
