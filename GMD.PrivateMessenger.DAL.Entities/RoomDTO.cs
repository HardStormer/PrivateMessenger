namespace GMD.PrivateMessenger.DAL.Entities;

public class RoomDTO : BaseDTO
{
    public List<UserDTO> Users { get; set; } = new List<UserDTO>();
    public List<MessageDTO> Messages { get; set; } = new List<MessageDTO>();
    public string? Name { get; set; }
}
