namespace HeyBuns.Api.Models;

public class Pedido
{
    public int Id { get; set; }
    public int SucursalId { get; set; }
    public Sucursal Sucursal { get; set; } = null!;

    // "Local" (pedido desde la mesa con QR) o "Domicilio"
    public string Tipo { get; set; } = "Local";
    public int? MesaId { get; set; }
    public Mesa? Mesa { get; set; }

    // Datos del cliente (el cliente no tiene cuenta)
    public string NombreCliente { get; set; } = "";
    public string? Telefono { get; set; }
    public string? Direccion { get; set; }

    // Pendiente -> Listo -> Entregado o Enviado, o Cancelado
    public string Estado { get; set; } = "Pendiente";
    public decimal Total { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
    public string? Notas { get; set; }
    public List<DetallePedido> Detalles { get; set; } = new();
}
