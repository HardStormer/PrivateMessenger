using GMD.PrivateMessenger.DAL.Interfaces;

namespace GMD.PrivateMessenger.PL.API.Models.Message.Queries.Get;

public class GetMessageQueryHandler : IRequestHandler<GetMessageQuery, MessageViewModel>
{
    private readonly IMessageRepository _service;
    private readonly IMapper _mapper;

    public GetMessageQueryHandler(
        IMessageRepository service,
        IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public async Task<MessageViewModel> Handle(GetMessageQuery request, CancellationToken cancellationToken)
    {
        var entity = await _service.GetAsync(request.Id);

        if (entity == null)
            throw new NotFoundException(nameof(MessageDTO), request.Id);

        var model = _mapper.Map<MessageViewModel>(entity);

        return model;
    }
}
