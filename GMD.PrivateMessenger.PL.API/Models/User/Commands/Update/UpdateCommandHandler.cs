using GMD.PrivateMessenger.DAL.Interfaces;
using GMD.PrivateMessenger.PL.API.Helpers;

namespace GMD.PrivateMessenger.PL.API.Models.User.Commands.Update;

public class UpdateUserCommandHandler :
    IRequestHandler<UpdateUserPasswordCommand>,
    IRequestHandler<UpdateUserNameCommand>
{
    private readonly IUserRepository _service;
    private readonly IValidator<UpdateUserPasswordCommand> _validatorPassword;
    private readonly IValidator<UpdateUserNameCommand> _validatorLogin;

    public UpdateUserCommandHandler(
        IUserRepository service,
        IValidator<UpdateUserPasswordCommand> validatorPassword,
        IValidator<UpdateUserNameCommand> validatorLogin)
    {
        _service = service;
        _validatorPassword = validatorPassword;
        _validatorLogin = validatorLogin;
    }

    public async Task Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
    {
        await _validatorPassword.ValidateAndThrowAsync(request, cancellationToken);

        var user = await _service.GetAsync(request.UserId);

        if (user == null)
            throw new NotFoundException(request.UserId.ToString(), request.UserId);

        if (user.Password != Additional.GetPasswordHash(request.OldPassword))
            throw new ExpectedException("Wrong password", HttpStatusCode.Forbidden);

        user.Password = Additional.GetPasswordHash(request.Password);

        await _service.EditAsync(user);
    }
    public async Task Handle(UpdateUserNameCommand request, CancellationToken cancellationToken)
    {
        await _validatorLogin.ValidateAndThrowAsync(request, cancellationToken);

        var user = await _service.GetAsync(request.UserId);

        if (user == null)
            throw new NotFoundException(request.UserId.ToString(), request.UserId);

        user.Name = request.Name;

        await _service.EditAsync(user);
    }
}
