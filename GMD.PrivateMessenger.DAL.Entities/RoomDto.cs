namespace GMD.PrivateMessenger.DAL.Entities;

public class RoomDto : BaseDto
{
    public ICollection<UserDto> Users { get; set; } = new List<UserDto>();
    public ICollection<MessageDto> Messages { get; set; } = new List<MessageDto>();
    public string? Name { get; set; }
}
