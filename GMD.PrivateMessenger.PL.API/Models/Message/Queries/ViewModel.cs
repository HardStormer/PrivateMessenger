namespace GMD.PrivateMessenger.PL.API.Models.Message.Queries;

public class MessageViewModel : BaseViewModel
{
    public string Text { get; set; } = string.Empty;
    public bool IsRead { get; set; } = false;
    public bool IsEdited { get; set; } = false;
}
