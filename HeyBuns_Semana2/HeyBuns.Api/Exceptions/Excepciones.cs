namespace HeyBuns.Api.Exceptions;

// Datos inválidos o regla de negocio que no se cumple (400)
public class ReglaNegocioException : Exception
{
    public ReglaNegocioException(string mensaje) : base(mensaje) { }
}

// No existe el registro buscado (404)
public class NoEncontradoException : Exception
{
    public NoEncontradoException(string mensaje) : base(mensaje) { }
}

// Correo o contraseña incorrectos (401)
public class CredencialesInvalidasException : Exception
{
    public CredencialesInvalidasException() : base("Correo o contraseña incorrectos") { }
}
