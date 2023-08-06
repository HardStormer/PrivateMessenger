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
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var dto = new MessageDto
        {
            Id = request.MessageId,
            IsEdited = true,
            Text = request.Text
        };

        await _service.PatchAsync(dto);
    }
}
