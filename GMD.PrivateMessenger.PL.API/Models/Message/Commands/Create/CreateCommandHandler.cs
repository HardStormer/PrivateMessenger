using GMD.PrivateMessenger.DAL.Interfaces;

namespace GMD.PrivateMessenger.PL.API.Models.Message.Commands.Create;

public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, Guid>
{
    private readonly IMessageRepository _service;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateMessageCommand> _validator;

    public CreateMessageCommandHandler(
        IMessageRepository service,
        IMapper mapper,
        IValidator<CreateMessageCommand> validator)
    {
        _service = service;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Guid> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);
        var DTO = _mapper.Map<MessageDTO>(request);
        var result = await _service.AddAsync(DTO);

        return result.Id;
    }
}
