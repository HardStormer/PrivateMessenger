namespace GMD.PrivateMessenger.PL.API.Models.Message.Commands.Delete;

public class DeleteMessageCommand : BaseDeleteCommand
{
    public Guid MessageId { get; set; }
}
