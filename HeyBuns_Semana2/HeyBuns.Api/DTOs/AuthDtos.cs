using System.ComponentModel.DataAnnotations;

namespace HeyBuns.Api.DTOs;

public class LoginDto
{
    [Required(ErrorMessage = "El correo es obligatorio")]
    public string Correo { get; set; } = "";

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    public string Password { get; set; } = "";
}

public class AuthRespuestaDto
{
    public string Token { get; set; } = "";
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string Correo { get; set; } = "";
    public string Rol { get; set; } = "";
    public int? SucursalId { get; set; }
    public string? SucursalNombre { get; set; }
}
