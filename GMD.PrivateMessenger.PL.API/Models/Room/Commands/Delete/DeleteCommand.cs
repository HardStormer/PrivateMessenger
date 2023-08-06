namespace GMD.PrivateMessenger.PL.API.Models.Room.Commands.Delete;

public class DeleteRoomCommand : BaseDeleteCommand
{
    public Guid RoomId { get; set; }
}
