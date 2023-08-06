using GMD.PrivateMessenger.DAL.Interfaces;
using GMD.PrivateMessenger.PL.API.Authentication;
using GMD.PrivateMessenger.PL.API.Models.Message.Queries;

namespace GMD.PrivateMessenger.PL.API.Models.Message.Commands.Create;

public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, MessageViewModel>
{
    private readonly IMessageRepository _service;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateMessageCommand> _validator;
    private readonly IHttpContextAccessor _contextAccessor;

    public CreateMessageCommandHandler(
        IMessageRepository service,
        IMapper mapper,
        IValidator<CreateMessageCommand> validator,
        IHttpContextAccessor contextAccessor)
    {
        _service = service;
        _mapper = mapper;
        _validator = validator;
        _contextAccessor = contextAccessor;
    }

    public async Task<MessageViewModel> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        var user = _contextAccessor.GetApiUser();

        if (user == null)
            throw new ExpectedException("User not found", HttpStatusCode.Forbidden);

        var dto = _mapper.Map<MessageDto>(request);
        
        dto.Id = Guid.NewGuid();
        dto.User = user;
        dto.UserId = user.Id;
        
        var resultDto = await _service.AddAsync(dto);
        var result = _mapper.Map<MessageViewModel>(resultDto);
        return result;
    }
}
