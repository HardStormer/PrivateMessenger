namespace GMD.PrivateMessenger.PL.API.Models.User.Queries;

public class UserViewModel : BaseViewModel
{
    public string? Name { get; set; }
    public string Login { get; set; } = string.Empty; 
    public string Bio { get; set; } = string.Empty;
}
