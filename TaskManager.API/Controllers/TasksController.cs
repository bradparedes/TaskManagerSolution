using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.DTOs;
using TaskManager.Core.Entities;
using TaskManager.Infrastructure.Data;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = $"{Roles.User},{Roles.Admin}")]

        public IActionResult GetTasks()
        {
            var tasks = _context.Tasks.ToList();
            return Ok(new[] { "Task1", "Task2" });
        }

        [HttpGet("all")]// Ruta explícita para evitar conflicto
        [Authorize(Roles = "User,Admin")]
        public IActionResult GetAll()
        {
            var tasks = _context.Tasks.ToList();
            return Ok(tasks);
        }


        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetById(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(TaskDto dto)
        {
            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate,
                IsCompleted = dto.IsCompleted,
                UserId = 1 // Temporal: hasta que se implemente autenticación
            };

            _context.Tasks.Add(task);
            _context.SaveChanges();

            return Ok(task);
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update(int id, TaskDto dto)
        {
            var task = _context.Tasks.Find(id);
            if (task == null) return NotFound();

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.DueDate = dto.DueDate;
            task.IsCompleted = dto.IsCompleted;

            _context.SaveChanges();

            return Ok(task);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null) return NotFound();

            _context.Tasks.Remove(task);
            _context.SaveChanges();

            return Ok("Tarea eliminada");
        }

        [HttpGet("user/{userId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllUsers()
        {
            var users = _context.Users
                .Select(u => new { u.Id, u.Username, u.Role })
                .ToList();

            return Ok(users);
        }
    }
}
