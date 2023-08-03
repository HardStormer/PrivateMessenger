namespace GMD.PrivateMessenger.PL.API.Models.User.Commands.Delete;

using GMD.PrivateMessenger.PL.API.Models;

public class DeleteUserCommand : CRUDCommand,
    IRequest
{
    public Guid UserId { get; set; }
    public bool Soft { get; set; } = false;
}