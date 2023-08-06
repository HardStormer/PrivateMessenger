namespace GMD.PrivateMessenger.DAL.Entities;

public class UserDto : BaseDto
{
    public string? Name { get; set; }
    public string Login { get; set; } = string.Empty; 
    public string Password { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public DateTime? TokenExpiredAt { get; set; }
    public ICollection<RoomDto> Rooms { get; set; } = new List<RoomDto>();
    public ICollection<MessageDto> Messages { get; set; } = new List<MessageDto>();
}
