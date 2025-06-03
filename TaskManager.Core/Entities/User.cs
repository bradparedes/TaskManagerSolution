using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string Role { get; set; } = Roles.User; // Default role
    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}
public static class Roles
{
    public const string Admin = "Admin";
    public const string User = "User";
}