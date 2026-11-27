namespace HeyBuns.Api.Models;

// Ahora los usuarios son solo trabajadores (Admin, Cocinero o Mesero)
public class Usuario
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string Correo { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public string Rol { get; set; } = Roles.Cocinero;

    // El cocinero y el mesero pertenecen a una sucursal, el admin no (puede entrar a las dos)
    public int? SucursalId { get; set; }
    public Sucursal? Sucursal { get; set; }

    public DateTime FechaRegistro { get; set; } = DateTime.Now;
}
