using HeyBuns.Api.Models;

namespace HeyBuns.Api.Data;

// Datos iniciales para poder probar la API sin capturar todo a mano
public static class DbSeeder
{
    public static void Sembrar(AppDbContext db)
    {
        if (!db.Sucursales.Any())
        {
            var valle = new Sucursal { Nombre = "Valle Alto", Direccion = "Dirección por definir", Telefono = "6670000001", Horario = "12:00 a 22:00" };
            var conquista = new Sucursal { Nombre = "La Conquista", Direccion = "Dirección por definir", Telefono = "6670000002", Horario = "12:00 a 22:00" };
            db.Sucursales.AddRange(valle, conquista);

            // 5 mesas por sucursal
            for (var n = 1; n <= 5; n++)
            {
                db.Mesas.Add(new Mesa { Sucursal = valle, Numero = n });
                db.Mesas.Add(new Mesa { Sucursal = conquista, Numero = n });
            }

            // Administrador y empleados de prueba
            var claveAdmin = BCrypt.Net.BCrypt.HashPassword("Admin123!");
            var claveEmpleado = BCrypt.Net.BCrypt.HashPassword("Empleado123!");
            db.Usuarios.AddRange(
                new Usuario { Nombre = "Administrador", Correo = "admin@heybuns.com", PasswordHash = claveAdmin, Rol = Roles.Admin },
                new Usuario { Nombre = "Cocinero Valle Alto", Correo = "cocinero.valle@heybuns.com", PasswordHash = claveEmpleado, Rol = Roles.Cocinero, Sucursal = valle },
                new Usuario { Nombre = "Mesero Valle Alto", Correo = "mesero.valle@heybuns.com", PasswordHash = claveEmpleado, Rol = Roles.Mesero, Sucursal = valle },
                new Usuario { Nombre = "Cocinero La Conquista", Correo = "cocinero.conquista@heybuns.com", PasswordHash = claveEmpleado, Rol = Roles.Cocinero, Sucursal = conquista },
                new Usuario { Nombre = "Mesero La Conquista", Correo = "mesero.conquista@heybuns.com", PasswordHash = claveEmpleado, Rol = Roles.Mesero, Sucursal = conquista }
            );
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

        db.SaveChanges();
    }
}
