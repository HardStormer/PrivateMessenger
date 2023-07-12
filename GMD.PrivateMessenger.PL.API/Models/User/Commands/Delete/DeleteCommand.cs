namespace GMD.PrivateMessenger.PL.API.Models.User.Commands.Delete;

using GMD.PrivateMessenger.PL.API.Models;

public class DeleteUserCommand : BaseCommand,
    IRequest
{
    public bool Soft { get; set; } = false;
}
