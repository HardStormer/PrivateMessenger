namespace GMD.PrivateMessenger.PL.API.Models.Room.Commands.Update;

public class UpdateRoomCommand : CRUDCommand,
    IRequest
{
    public Guid RoomId { get; set; }
    public string? Name { get; set; }
}
public class UpdateRoomCommandValidator : AbstractValidator<UpdateRoomCommand>
{
    public UpdateRoomCommandValidator()
    {
    }
}
