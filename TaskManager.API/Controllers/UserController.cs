using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.API.DTOs;
using TaskManager.Core.Entities;
using TaskManager.Infrastructure.Data;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        [AllowAnonymous] // Permitir registro sin autenticación
        public IActionResult Register(UserRegisterDto dto)
        {
            if (_context.Users.Any(u => u.Email == dto.Email))
                return BadRequest("El usuario ya existe");

            // Genera un salt simple (para este ejemplo)
            var salt = Guid.NewGuid().ToString();
            var hashedPassword = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(dto.Password + salt));

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = hashedPassword,
                Role = "User" // Asignar rol por defecto
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(new { message = "Usuario Creado", role = user.Role });
        }

        [HttpPost("login")]
        [Authorize]
        public IActionResult Login(UserLoginDto dto)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == dto.Email);
            if (user == null) return Unauthorized("Credenciales inválidas");

            var hashedPassword = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(dto.Password));

            if (user.PasswordHash != hashedPassword)
                return Unauthorized("Credenciales inválidas");

            return Ok("Login exitoso");
        }

        [HttpGet("all")]
        [Authorize(Roles = "Admin")] // Protege este endpoint solo para Admins
        public IActionResult GetAllUsers()
        {
            var users = _context.Users
                .Select(u => new UserResponseDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    Role = u.Role,
                    CreatedAt = u.CreatedAt
                })
                .ToList();

            return Ok(users);
        }

        // GET: api/User
        [HttpGet]
        [Authorize]
        public IActionResult GetUsers()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }
        
        // POST: api/User
        [HttpPost]
        [Authorize]
        public IActionResult CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.Admin)]      // Solo admins pueden listar y gestionar usuarios
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET api/users
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _context.Users
                .Select(u => new {
                    u.Id,
                    u.Username,
                    u.Email,
                    u.Role,
                    u.CreatedAt
                })
                .ToListAsync();
            return Ok(users);
        }

        // GET api/users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var u = await _context.Users
                .Where(x => x.Id == id)
                .Select(u => new {
                    u.Id,
                    u.Username,
                    u.Email,
                    u.Role,
                    u.CreatedAt
                })
                .FirstOrDefaultAsync();

            if (u == null) return NotFound();
            return Ok(u);
        }

        // DELETE api/users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
