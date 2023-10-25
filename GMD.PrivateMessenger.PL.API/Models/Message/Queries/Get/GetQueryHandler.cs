using GMD.PrivateMessenger.DAL.Interfaces;
using GMD.PrivateMessenger.PL.API.Authentication;

namespace GMD.PrivateMessenger.PL.API.Models.Message.Queries.Get;

public class GetMessageQueryHandler : IRequestHandler<GetMessageQuery, MessageViewModel>
{
    private readonly IMessageRepository _service;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetMessageQueryHandler(
        IMessageRepository service,
        IMapper mapper, 
        IHttpContextAccessor contextAccessor)
    {
        _service = service;
        _mapper = mapper;
        _contextAccessor = contextAccessor;
    }

    public async Task<MessageViewModel> Handle(GetMessageQuery request, CancellationToken cancellationToken)
    {
        var userId = _contextAccessor.GetApiUserId();
        
        var entity = await _service.GetAsync(request.Id, new[]{"User"});

        if (entity == null)
            throw new NotFoundException(nameof(MessageDto), request.Id);

        var model = _mapper.Map<MessageViewModel>(entity);

        if (userId == null) return model;
        
        if (model.User.Id == userId)
        {
            model.IsMy = true;
        }
        return model;
    }
}
