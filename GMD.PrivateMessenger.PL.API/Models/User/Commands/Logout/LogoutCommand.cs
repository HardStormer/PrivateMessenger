namespace GMD.PrivateMessenger.PL.API.Models.User.Commands.Logout;

public class LogoutUserCommand : 
    IRequest
{
    public Guid UserId { get; set; }
}