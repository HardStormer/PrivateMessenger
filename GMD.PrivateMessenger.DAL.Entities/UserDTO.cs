namespace GMD.PrivateMessenger.DAL.Entities;

public class UserDTO : BaseDTO
{
    public string? Name { get; set; }
    public string Login { get; set; } = string.Empty; 
    public string Password { get; set; } = string.Empty; 
    public DateTime? TokenExpiredAt { get; set; }
}
