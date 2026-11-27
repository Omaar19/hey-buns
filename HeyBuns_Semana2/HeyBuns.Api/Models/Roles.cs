namespace HeyBuns.Api.Models;

public static class Roles
{
    public const string Admin = "Admin";
    public const string Cocinero = "Cocinero";
    public const string Mesero = "Mesero";

    public static readonly string[] Todos = { Admin, Cocinero, Mesero };
}
