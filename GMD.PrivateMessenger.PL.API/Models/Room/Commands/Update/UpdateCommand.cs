namespace GMD.PrivateMessenger.PL.API.Models.Room.Commands.Update;

public class UpdateRoomCommand : BaseCommand,
    IRequest
{
    public string? Name { get; set; }
}
public class UpdateRoomCommandValidator : AbstractValidator<UpdateRoomCommand>
{
    public UpdateRoomCommandValidator()
    {
    }
}
