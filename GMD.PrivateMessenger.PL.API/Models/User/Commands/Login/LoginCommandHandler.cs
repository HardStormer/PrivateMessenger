using GMD.PrivateMessenger.DAL.Interfaces;
using GMD.PrivateMessenger.PL.API.Authentication;
using GMD.PrivateMessenger.PL.API.Helpers;

namespace GMD.PrivateMessenger.PL.API.Models.User.Commands.Login;

public class LoginUserCommandHandler :
    IRequestHandler<LoginUserCommand, string>
{
    private readonly IUserRepository _service;
    private readonly IMapper _mapper;
    private readonly IValidator<LoginUserCommand> _validator;

    public LoginUserCommandHandler(IUserRepository service, IValidator<LoginUserCommand> validator, IMapper mapper)
    {
        _service = service;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);

        var user = await _service.GetAsync(request.Login);

        if (user == null)
            throw new ExpectedException("Wrong login", HttpStatusCode.Forbidden);

        if (user.Password != Additional.GetPasswordHash(request.Password))
            throw new ExpectedException("Wrong password", HttpStatusCode.Forbidden);

        user.TokenExpiredAt = DateTime.UtcNow.AddDays(1);

        await _service.EditAsync(user);

        var token = user.GetToken();

        return token;
    }
}
