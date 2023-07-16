namespace GMD.PrivateMessenger.PL.API.Models.Message.Commands.Update;

public class UpdateMessageCommand : CRUDCommand,
    IRequest
{
    public Guid MessageId { get; set; }
    public string Text { get; set; } = string.Empty;
}
public class UpdateMessageCommandValidator : AbstractValidator<UpdateMessageCommand>
{
    public UpdateMessageCommandValidator()
    {
    }
}
