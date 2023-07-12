using GMD.PrivateMessenger.DAL.Interfaces;

namespace GMD.PrivateMessenger.PL.API.Models.User.Commands.Delete;

public class DeleteUserCommandHandler :
    IRequestHandler<DeleteUserCommand>
{
    private readonly IUserRepository _service;

    public DeleteUserCommandHandler(IUserRepository service)
    {
        _service = service;
    }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await _service.RemoveAsync(request.Id, request.Soft);
    }
}
