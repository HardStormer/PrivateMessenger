namespace GMD.PrivateMessenger.PL.API.Models.User.Commands.Logout;

using GMD.PrivateMessenger.PL.API.Models;

public class LogoutUserCommand : 
    IRequest
{
    public Guid UserId { get; set; }
}