namespace GMD.PrivateMessenger.PL.API.Models.User.Commands.Delete;

public class DeleteUserCommand : BaseDeleteCommand
{
    public Guid UserId { get; set; }
}