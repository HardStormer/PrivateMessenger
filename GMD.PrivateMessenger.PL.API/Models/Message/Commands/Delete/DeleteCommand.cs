namespace GMD.PrivateMessenger.PL.API.Models.Message.Commands.Delete;

using GMD.PrivateMessenger.PL.API.Models;

public class DeleteMessageCommand : BaseCommand,
    IRequest
{
    public bool Soft { get; set; } = false;
}
