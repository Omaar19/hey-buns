using HeyBuns.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HeyBuns.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Sucursal> Sucursales => Set<Sucursal>();
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Producto> Productos => Set<Producto>();
    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<DetallePedido> DetallesPedido => Set<DetallePedido>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(e =>
        {
            e.Property(u => u.Nombre).HasMaxLength(100).IsRequired();
            e.Property(u => u.Correo).HasMaxLength(150).IsRequired();
            e.Property(u => u.PasswordHash).HasMaxLength(200).IsRequired();
            e.Property(u => u.Rol).HasMaxLength(20).IsRequired();
            e.HasIndex(u => u.Correo).IsUnique();
        });

        modelBuilder.Entity<Sucursal>(e =>
        {
            e.Property(s => s.Nombre).HasMaxLength(100).IsRequired();
            e.Property(s => s.Direccion).HasMaxLength(200).IsRequired();
            e.Property(s => s.Telefono).HasMaxLength(20);
            e.Property(s => s.Horario).HasMaxLength(100).IsRequired();
        });

        modelBuilder.Entity<Categoria>(e =>
        {
            e.Property(c => c.Nombre).HasMaxLength(50).IsRequired();
            e.HasIndex(c => c.Nombre).IsUnique();
        });

        modelBuilder.Entity<Producto>(e =>
        {
            e.Property(p => p.Nombre).HasMaxLength(100).IsRequired();
            e.Property(p => p.Descripcion).HasMaxLength(300);
            e.Property(p => p.ImagenUrl).HasMaxLength(300);
            e.Property(p => p.Precio).HasColumnType("decimal(10,2)");
            e.HasOne(p => p.Categoria).WithMany().HasForeignKey(p => p.CategoriaId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Pedido>(e =>
        {
            e.Property(p => p.Estado).HasMaxLength(20).IsRequired();
            e.Property(p => p.Notas).HasMaxLength(200);
            e.Property(p => p.Total).HasColumnType("decimal(10,2)");
            e.HasOne(p => p.Usuario).WithMany().HasForeignKey(p => p.UsuarioId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(p => p.Sucursal).WithMany().HasForeignKey(p => p.SucursalId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<DetallePedido>(e =>
        {
            e.ToTable("DetallePedido");
            e.Property(d => d.PrecioUnitario).HasColumnType("decimal(10,2)");
            e.HasOne(d => d.Pedido).WithMany(p => p.Detalles).HasForeignKey(d => d.PedidoId);
            e.HasOne(d => d.Producto).WithMany().HasForeignKey(d => d.ProductoId).OnDelete(DeleteBehavior.Restrict);
        });
    }
}
