using System.ComponentModel.DataAnnotations;

namespace HeyBuns.Api.DTOs;

public class SucursalDto
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [MaxLength(100)]
    public string Nombre { get; set; } = "";

    [Required(ErrorMessage = "La dirección es obligatoria")]
    [MaxLength(200)]
    public string Direccion { get; set; } = "";

    [MaxLength(20)]
    public string? Telefono { get; set; }

    [Required(ErrorMessage = "El horario es obligatorio")]
    [MaxLength(100)]
    public string Horario { get; set; } = "";

    public bool Activa { get; set; } = true;
}
