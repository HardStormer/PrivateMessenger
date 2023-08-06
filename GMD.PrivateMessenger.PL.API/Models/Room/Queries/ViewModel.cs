using GMD.PrivateMessenger.PL.API.Models.User.Queries;

namespace GMD.PrivateMessenger.PL.API.Models.Room.Queries;

public class RoomViewModel : BaseViewModel
{
    public List<UserViewModel> Users { get; set; } = new();
    public string? Name { get; set; }
}
