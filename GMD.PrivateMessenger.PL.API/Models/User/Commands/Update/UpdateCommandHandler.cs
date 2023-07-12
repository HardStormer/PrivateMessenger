using GMD.PrivateMessenger.DAL.Interfaces;
using GMD.PrivateMessenger.PL.API.Helpers;

namespace GMD.PrivateMessenger.PL.API.Models.User.Commands.Update;

public class UpdateUserCommandHandler :
    IRequestHandler<UpdateUserPasswordCommand>,
    IRequestHandler<UpdateUserNameCommand>
{
    private readonly IUserRepository _service;
    private readonly IMapper _mapper;
    private readonly IValidator<UpdateUserPasswordCommand> _validatorPassword;
    private readonly IValidator<UpdateUserNameCommand> _validatorLogin;

    public UpdateUserCommandHandler(
        IUserRepository service,
        IMapper mapper,
        IValidator<UpdateUserPasswordCommand> validatorPassword,
        IValidator<UpdateUserNameCommand> validatorLogin)
    {
        _service = service;
        _mapper = mapper;
        _validatorPassword = validatorPassword;
        _validatorLogin = validatorLogin;
    }

    public async Task Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
    {
        _validatorPassword.ValidateAndThrow(request);

        var user = await _service.GetAsync(request.Id);

        if (user == null)
            throw new NotFoundException(request.Id.ToString(), request.Id);

        if (user.Password != Additional.GetPasswordHash(request.OldPassword))
            throw new ExpectedException("Wrong password", HttpStatusCode.Forbidden);

        user.Password = Additional.GetPasswordHash(request.Password);

        await _service.EditAsync(user);
    }
    public async Task Handle(UpdateUserNameCommand request, CancellationToken cancellationToken)
    {
        _validatorLogin.ValidateAndThrow(request);

        var user = await _service.GetAsync(request.Id);

        if (user == null)
            throw new NotFoundException(request.Id.ToString(), request.Id);

        user.Name = request.Name;

        await _service.EditAsync(user);
    }
}
