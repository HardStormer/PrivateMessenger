using GMD.PrivateMessenger.DAL.Interfaces;

namespace GMD.PrivateMessenger.PL.API.Models.User.Commands.Logout;

public class LogoutUserCommandHandler :
    IRequestHandler<LogoutUserCommand>
{
    private readonly IUserRepository _service;

    public LogoutUserCommandHandler(IUserRepository service)
    {
        _service = service;
    }

    public async Task Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _service.GetAsync(request.UserId);

        if (user == null)
            throw new NotFoundException(request.UserId.ToString(), request.UserId);

        user.TokenExpiredAt = DateTime.UtcNow;

        await _service.EditAsync(user);
    }
}
