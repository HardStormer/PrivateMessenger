using GMD.PrivateMessenger.DAL.Interfaces;
using GMD.PrivateMessenger.PL.API.Authentication;
using GMD.PrivateMessenger.PL.API.Helpers;

namespace GMD.PrivateMessenger.PL.API.Models.User.Commands.Logout;

public class LogoutUserCommandHandler :
    IRequestHandler<LogoutUserCommand>
{
    private readonly IUserRepository _service;
    private readonly IMapper _mapper;

    public LogoutUserCommandHandler(IUserRepository service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
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
