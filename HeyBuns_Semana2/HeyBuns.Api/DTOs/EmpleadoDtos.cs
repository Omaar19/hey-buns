using System.ComponentModel.DataAnnotations;

namespace HeyBuns.Api.DTOs;

public class EmpleadoDto
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [MaxLength(100)]
    public string Nombre { get; set; } = "";

    [Required(ErrorMessage = "El correo es obligatorio")]
    [EmailAddress(ErrorMessage = "El correo no es válido")]
    [MaxLength(150)]
    public string Correo { get; set; } = "";

    // Obligatoria al crear, al editar solo se manda si se quiere cambiar
    [MaxLength(100)]
    public string? Password { get; set; }

    [Required(ErrorMessage = "El rol es obligatorio")]
    public string Rol { get; set; } = "";

    // Obligatoria para Cocinero y Mesero, el Admin no lleva sucursal
    public int? SucursalId { get; set; }
}

public record EmpleadoRespuestaDto(
    int Id,
    string Nombre,
    string Correo,
    string Rol,
    int? SucursalId,
    string? Sucursal);
