namespace TaskManager.API.DTOs
{
    public class UserRegisterDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty!;
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = "User"; // Default role
    }
    
}
