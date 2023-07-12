namespace GMD.PrivateMessenger.PL.API.Models.Room.Queries;

public class RoomViewModel : BaseViewModel
{
    public List<UserDTO>? Users { get; set; }
    public List<MessageDTO>? Messages { get; set; }
    public string? Name { get; set; }
}
