using GMD.PrivateMessenger.DAL.Interfaces;

namespace GMD.PrivateMessenger.PL.API.Models.Message.Commands.Update;

public class UpdateMessageCommandHandler :
    IRequestHandler<UpdateMessageCommand>
{
    private readonly IMessageRepository _service;
    private readonly IMapper _mapper;
    private readonly IValidator<UpdateMessageCommand> _validator;

    public UpdateMessageCommandHandler(
        IMessageRepository service,
        IMapper mapper,
        IValidator<UpdateMessageCommand> validator)
    {
        _service = service;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);

        var DTO = _mapper.Map<MessageDTO>(request);

        DTO.IsEdited = true;

        await _service.PatchAsync(DTO);
    }
}
