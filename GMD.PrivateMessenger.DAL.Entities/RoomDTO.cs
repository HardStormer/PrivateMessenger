namespace GMD.PrivateMessenger.DAL.Entities;

public class RoomDTO : BaseDTO
{
    public List<UserDTO>? Users { get; set; }
    public List<MessageDTO>? Messages { get; set; }
    public string? Name { get; set; }
}
