using HeyBuns.Api.Models;

namespace HeyBuns.Api.Data;

// Datos iniciales para poder probar la API sin capturar todo a mano
public static class DbSeeder
{
    public static void Sembrar(AppDbContext db)
    {
        if (!db.Usuarios.Any())
        {
            db.Usuarios.Add(new Usuario
            {
                Nombre = "Administrador",
                Correo = "admin@heybuns.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                Rol = "Admin"
            });
        }

        if (!db.Categorias.Any())
        {
            var hamburguesas = new Categoria { Nombre = "Hamburguesas" };
            var bebidas = new Categoria { Nombre = "Bebidas" };
            var complementos = new Categoria { Nombre = "Complementos" };
            db.Categorias.AddRange(hamburguesas, bebidas, complementos);

            db.Productos.AddRange(
                new Producto { Categoria = hamburguesas, Nombre = "Clásica", Descripcion = "Carne de res, lechuga, jitomate y queso", Precio = 79 },
                new Producto { Categoria = hamburguesas, Nombre = "Doble queso", Descripcion = "Doble carne con doble queso", Precio = 99 },
                new Producto { Categoria = hamburguesas, Nombre = "Bacon", Descripcion = "Carne de res con tocino crujiente", Precio = 109 },
                new Producto { Categoria = bebidas, Nombre = "Refresco", Descripcion = "Refresco de 355 ml", Precio = 25 },
                new Producto { Categoria = complementos, Nombre = "Papas", Descripcion = "Papas a la francesa", Precio = 35 }
            );
        }

        if (!db.Sucursales.Any())
        {
            db.Sucursales.AddRange(
                new Sucursal { Nombre = "Hey Buns Centro", Direccion = "Dirección por definir", Telefono = "6670000001", Horario = "12:00 a 22:00" },
                new Sucursal { Nombre = "Hey Buns Norte", Direccion = "Dirección por definir", Telefono = "6670000002", Horario = "12:00 a 22:00" }
            );
        }

        db.SaveChanges();
    }
}
