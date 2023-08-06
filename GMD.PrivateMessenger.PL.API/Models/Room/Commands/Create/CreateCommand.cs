using GMD.PrivateMessenger.PL.API.Models.Room.Queries;

namespace GMD.PrivateMessenger.PL.API.Models.Room.Commands.Create;

public class CreateRoomCommand : BaseCreateCommand<RoomViewModel>
{
    public string? Name { get; set; }
}
public class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
{
    public CreateRoomCommandValidator()
    {
    }
}
