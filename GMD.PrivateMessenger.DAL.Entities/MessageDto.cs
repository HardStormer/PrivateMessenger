namespace GMD.PrivateMessenger.DAL.Entities;

public class MessageDto : BaseDto
{
    public string Text { get; set; } = string.Empty;
    public bool IsRead { get; set; } = false;
    public bool IsEdited { get; set; } = false;
    public Guid UserId { get; set; }
    public UserDto User { get; set; }
    public Guid RoomId { get; set; }
}
