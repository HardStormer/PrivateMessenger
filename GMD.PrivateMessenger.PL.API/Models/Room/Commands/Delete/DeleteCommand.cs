namespace GMD.PrivateMessenger.PL.API.Models.Room.Commands.Delete;

using GMD.PrivateMessenger.PL.API.Models;

public class DeleteRoomCommand : BaseCommand,
    IRequest
{
    public bool Soft { get; set; } = false;
}
