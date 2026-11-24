namespace HeyBuns.Api.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string Correo { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public string Rol { get; set; } = "Cliente";
    public DateTime FechaRegistro { get; set; } = DateTime.Now;
}
