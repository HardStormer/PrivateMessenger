namespace GMD.PrivateMessenger.DAL.Entities;

public class UserDTO : BaseDTO
{
    public string? Name { get; set; }
    public string Login { get; set; } = string.Empty; 
    public string Password { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public DateTime? TokenExpiredAt { get; set; }
    public List<RoomDTO> Rooms { get; set; } = new List<RoomDTO>();
    public List<MessageDTO> Messages { get; set; } = new List<MessageDTO>();
}
