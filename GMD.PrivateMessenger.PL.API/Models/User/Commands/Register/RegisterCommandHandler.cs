using GMD.PrivateMessenger.DAL.Interfaces;
using GMD.PrivateMessenger.PL.API.Authentication;
using GMD.PrivateMessenger.PL.API.Helpers;

namespace GMD.PrivateMessenger.PL.API.Models.User.Commands.Register;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserCommandResponce>
{
    private readonly IUserRepository _service;
    private readonly IMapper _mapper;
    private readonly IValidator<RegisterUserCommand> _validator;

    public RegisterUserCommandHandler(
        IUserRepository service,
        IMapper mapper,
        IValidator<RegisterUserCommand> validator)
    {
        _service = service;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<RegisterUserCommandResponce> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);

        var user = await _service.GetAsync(request.Login);

        if (user != null)
            throw new ExpectedException("Login already exists", HttpStatusCode.Forbidden);

        var DTO = _mapper.Map<UserDTO>(request);

        DTO.Password = Additional.GetPasswordHash(DTO.Password);
        DTO.TokenExpiredAt = DateTime.UtcNow.AddDays(1);

        var result = await _service.AddAsync(DTO);

        var token = result.GetToken();

        var responce = new RegisterUserCommandResponce()
        {
            Token = token
        };

        return responce;
    }
}
