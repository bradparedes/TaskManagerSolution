using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManager.API.DTOs;
using TaskManager.Core.Entities;
using TaskManager.Infrastructure.Data;
using TaskManager.Infrastructure.Services;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ITokenService _tokenService;

        public AuthController(AppDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody] UserRegisterDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
                return BadRequest("El email es obligatorio.");

            if (string.IsNullOrWhiteSpace(request.Username))
                return BadRequest("El nombre de usuario es obligatorio.");

            if (string.IsNullOrWhiteSpace(request.Password))
                return BadRequest("La contraseña es obligatoria.");

            if (_context.Users.Any(u => u.Username == request.Username))
                return BadRequest("El usuario ya existe.");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = hashedPassword,
                CreatedAt = DateTime.UtcNow,
                Role = request.Role ?? Roles.User
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(new { message = "Usuario registrado exitosamente." });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UserLoginDto login)
        {
            if (string.IsNullOrWhiteSpace(login.Username) || string.IsNullOrWhiteSpace(login.Password))
                return BadRequest("Usuario y contraseña son requeridos.");

            var user = _context.Users.FirstOrDefault(u => u.Username == login.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash))
                return Unauthorized("Credenciales inválidas.");

            var token = _tokenService.CreateToken(user);

            return Ok(new { token });
        }
    }
}