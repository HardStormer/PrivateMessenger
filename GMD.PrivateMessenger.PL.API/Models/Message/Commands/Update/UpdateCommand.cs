namespace GMD.PrivateMessenger.PL.API.Models.Message.Commands.Update;

public class UpdateMessageCommand : BaseCommand,
    IRequest
{
    public string Text { get; set; } = string.Empty;
}
public class UpdateMessageCommandValidator : AbstractValidator<UpdateMessageCommand>
{
    public UpdateMessageCommandValidator()
    {
    }
}
