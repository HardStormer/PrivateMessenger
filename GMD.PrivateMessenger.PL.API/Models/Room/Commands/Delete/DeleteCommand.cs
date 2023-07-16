namespace GMD.PrivateMessenger.PL.API.Models.Room.Commands.Delete;

using GMD.PrivateMessenger.PL.API.Models;

public class DeleteRoomCommand : CRUDCommand,
    IRequest
{
    public Guid Id { get; set; }
    public bool Soft { get; set; } = false;
}
