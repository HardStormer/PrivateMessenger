using GMD.PrivateMessenger.PL.API.Models.Message.Queries;

namespace GMD.PrivateMessenger.PL.API.Models.Message.Commands.Create;


public class CreateMessageCommand : BaseCreateCommand<MessageViewModel>
{
    public Guid RoomId { get; set; }
    public string Text { get; set; } = string.Empty;
}
public class CreateMessageCommandValidator : AbstractValidator<CreateMessageCommand>
{
    public CreateMessageCommandValidator()
    {
    }
}
