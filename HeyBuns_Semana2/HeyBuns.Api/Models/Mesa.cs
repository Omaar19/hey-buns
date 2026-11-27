namespace HeyBuns.Api.Models;

// Cada mesa tendrá su QR con la sucursal y el número de mesa
public class Mesa
{
    public int Id { get; set; }
    public int SucursalId { get; set; }
    public Sucursal Sucursal { get; set; } = null!;
    public int Numero { get; set; }
}
