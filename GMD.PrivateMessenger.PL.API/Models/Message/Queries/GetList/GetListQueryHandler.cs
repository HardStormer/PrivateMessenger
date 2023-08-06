using System.Linq.Expressions;
using GMD.PrivateMessenger.DAL.Interfaces;

namespace GMD.PrivateMessenger.PL.API.Models.Message.Queries.GetList;

public class GetMessageQueryListHandler : 
    IRequestHandler<GetMessageListQuery, MessageListViewModel>, 
    IRequestHandler<GetMessageListByTextQuery, MessageListViewModel>,
    IRequestHandler<GetMessageListByRoomIdQuery, MessageListViewModel>
{
    private readonly IMessageRepository _service;
    private readonly IMapper _mapper;

    public GetMessageQueryListHandler(
        IMessageRepository service,
        IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public async Task<MessageListViewModel> Handle(GetMessageListQuery request, CancellationToken cancellationToken)
    {
        var limit = request.Limit;
        var offset = request.Offset;
        Expression<Func<MessageDto, bool>>? filter = null;
        var includeProperties = new[]
        {
            "User"
        };

        var wrapper = await _service.GetAsync(limit, offset, filter, includeProperties);

        var entities = wrapper.Items;

        var models = _mapper.Map<IEnumerable<MessageViewModel>>(entities);

        var listView = new MessageListViewModel
        {
            ModelList = models,
            TotalCount = wrapper.TotalCount
        };

        return listView;
    }
    public async Task<MessageListViewModel> Handle(GetMessageListByTextQuery request, CancellationToken cancellationToken)
    {
        var limit = request.Limit;
        var offset = request.Offset;
        Expression<Func<MessageDto, bool>> filter = dto => dto.Text.Contains(request.Text);
        var includeProperties = new[]
        {
            "User"
        };

        var wrapper = await _service.GetAsync(limit, offset, filter, includeProperties);

        var entities = wrapper.Items;

        var models = _mapper.Map<IEnumerable<MessageViewModel>>(entities);

        var listView = new MessageListViewModel
        {
            ModelList = models,
            TotalCount = wrapper.TotalCount
        };

        return listView;
    }
    public async Task<MessageListViewModel> Handle(GetMessageListByRoomIdQuery request, CancellationToken cancellationToken)
    {
        var limit = request.Limit;
        var offset = request.Offset;
        Expression<Func<MessageDto, bool>> filter = dto => dto.RoomId == request.RoomId;
        var includeProperties = new[]
        {
            "User"
        };

        var wrapper = await _service.GetAsync(limit, offset, filter, includeProperties);

        var entities = wrapper.Items;

        var models = _mapper.Map<IEnumerable<MessageViewModel>>(entities);

        var listView = new MessageListViewModel
        {
            ModelList = models,
            TotalCount = wrapper.TotalCount
        };

        return listView;
    }
}
