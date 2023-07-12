using GMD.PrivateMessenger.DAL.Interfaces;

namespace GMD.PrivateMessenger.PL.API.Models.Room.Commands.Delete;

public class DeleteRoomCommandHandler :
    IRequestHandler<DeleteRoomCommand>
{
    private readonly IRoomRepository _service;

    public DeleteRoomCommandHandler(IRoomRepository service)
    {
        _service = service;
    }

    public async Task Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
    {
        await _service.RemoveAsync(request.Id, request.Soft);
    }
}
