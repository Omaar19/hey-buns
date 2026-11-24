using System.ComponentModel.DataAnnotations;

namespace HeyBuns.Api.DTOs;

public class RegistroDto
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [MaxLength(100)]
    public string Nombre { get; set; } = "";

    [Required(ErrorMessage = "El correo es obligatorio")]
    [EmailAddress(ErrorMessage = "El correo no es válido")]
    [MaxLength(150)]
    public string Correo { get; set; } = "";

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
    [MaxLength(100)]
    public string Password { get; set; } = "";
}

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
}
