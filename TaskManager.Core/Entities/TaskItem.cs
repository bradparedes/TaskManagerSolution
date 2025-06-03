using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.Entities;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty; // Asignar rol por defecto si no se especifica
    public bool IsCompleted { get; set; }
    public DateTime DueDate { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;
}
