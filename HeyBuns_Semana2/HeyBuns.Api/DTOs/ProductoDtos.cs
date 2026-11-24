using System.ComponentModel.DataAnnotations;

namespace HeyBuns.Api.DTOs;

public class ProductoDto
{
    [Range(1, int.MaxValue, ErrorMessage = "Debes elegir una categoría")]
    public int CategoriaId { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [MaxLength(100)]
    public string Nombre { get; set; } = "";

    [MaxLength(300)]
    public string? Descripcion { get; set; }

    [Range(0.01, 100000, ErrorMessage = "El precio debe ser mayor a 0")]
    public decimal Precio { get; set; }

    [MaxLength(300)]
    public string? ImagenUrl { get; set; }

    public bool Disponible { get; set; } = true;
}

public record ProductoRespuestaDto(
    int Id,
    int CategoriaId,
    string Categoria,
    string Nombre,
    string? Descripcion,
    decimal Precio,
    string? ImagenUrl,
    bool Disponible);
