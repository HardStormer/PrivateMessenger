using GMD.PrivateMessenger.DAL.Interfaces;

namespace GMD.PrivateMessenger.PL.API.Models.Message.Commands.Delete;

public class DeleteMessageCommandHandler :
    IRequestHandler<DeleteMessageCommand>
{
    private readonly IMessageRepository _service;

    public DeleteMessageCommandHandler(IMessageRepository service)
    {
        _service = service;
    }

    public async Task Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
    {
        await _service.RemoveAsync(request.MessageId, request.Soft);
    }
}
