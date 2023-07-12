namespace GMD.PrivateMessenger.PL.API.Models.Room.Commands.Create;


public class CreateRoomCommand : BaseCommand,
    IRequest<Guid>
{
    public string? Name { get; set; }
}
public class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
{
    public CreateRoomCommandValidator()
    {
    }
}
