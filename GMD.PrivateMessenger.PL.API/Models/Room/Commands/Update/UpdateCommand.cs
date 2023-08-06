namespace GMD.PrivateMessenger.PL.API.Models.Room.Commands.Update;

public class UpdateRoomCommand : BaseUpdateCommand
{
    public Guid RoomId { get; set; }
    public string Name { get; set; } = string.Empty;
}
public class UpdateRoomCommandValidator : AbstractValidator<UpdateRoomCommand>
{
    public UpdateRoomCommandValidator()
    {
    }
}
