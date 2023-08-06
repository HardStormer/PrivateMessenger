using GMD.PrivateMessenger.PL.API.Models.User.Queries;

namespace GMD.PrivateMessenger.PL.API.Models.Message.Queries;

public class MessageViewModel : BaseViewModel
{
    public UserViewModel User { get; set; } = new UserViewModel();
    public string Text { get; set; } = string.Empty;
    public bool IsRead { get; set; } = false;
    public bool IsEdited { get; set; } = false;
}
