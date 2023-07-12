namespace GMD.PrivateMessenger.PL.API.Models.Message.Commands.Create;


public class CreateMessageCommand : BaseCommand,
    IRequest<Guid>
{
    public string Text { get; set; } = string.Empty;
}
public class CreateMessageCommandValidator : AbstractValidator<CreateMessageCommand>
{
    public CreateMessageCommandValidator()
    {
    }
}
