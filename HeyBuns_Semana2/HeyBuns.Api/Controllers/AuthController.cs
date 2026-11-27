using HeyBuns.Api.DTOs;
using HeyBuns.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HeyBuns.Api.Controllers;

// Solo para trabajadores, los clientes no inician sesión
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;

    public AuthController(IAuthService auth) => _auth = auth;

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto) =>
        Ok(await _auth.LoginAsync(dto));
}
