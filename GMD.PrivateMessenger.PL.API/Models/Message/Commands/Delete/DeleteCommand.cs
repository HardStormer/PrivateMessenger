namespace GMD.PrivateMessenger.PL.API.Models.Message.Commands.Delete;

using GMD.PrivateMessenger.PL.API.Models;

public class DeleteMessageCommand : CRUDCommand,
    IRequest
{
    public Guid MessageId { get; set; }
    public bool Soft { get; set; } = false;
}
