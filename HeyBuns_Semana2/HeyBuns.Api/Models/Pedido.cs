namespace HeyBuns.Api.Models;

public class Pedido
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;
    public int SucursalId { get; set; }
    public Sucursal Sucursal { get; set; } = null!;
    public string Estado { get; set; } = "Pendiente";
    public decimal Total { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
    public string? Notas { get; set; }
    public List<DetallePedido> Detalles { get; set; } = new();
}
