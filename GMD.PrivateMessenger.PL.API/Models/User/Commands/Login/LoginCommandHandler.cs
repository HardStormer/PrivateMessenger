using GMD.PrivateMessenger.DAL.Interfaces;
using GMD.PrivateMessenger.PL.API.Authentication;
using GMD.PrivateMessenger.PL.API.Helpers;

namespace GMD.PrivateMessenger.PL.API.Models.User.Commands.Login;

public class LoginUserCommandHandler :
    IRequestHandler<LoginUserCommand, LoginUserCommandResponce>
{
    private readonly IUserRepository _service;
    private readonly IValidator<LoginUserCommand> _validator;

    public LoginUserCommandHandler(IUserRepository service, IValidator<LoginUserCommand> validator)
    {
        _service = service;
        _validator = validator;
    }

    public async Task<LoginUserCommandResponce> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var user = await _service.GetAsync(request.Login);

        if (user == null)
            throw new ExpectedException("Wrong login", HttpStatusCode.Forbidden);

        if (user.Password != Additional.GetPasswordHash(request.Password))
            throw new ExpectedException("Wrong password", HttpStatusCode.Forbidden);

        user.TokenExpiredAt = DateTime.UtcNow.AddDays(1);

        await _service.EditAsync(user);

        var token = user.GetToken();

        var responce = new LoginUserCommandResponce()
        {
            Token = token,
        };

        return responce;
    }
}
