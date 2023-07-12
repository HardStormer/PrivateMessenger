using System.Linq.Expressions;
using GMD.PrivateMessenger.DAL.Interfaces;

namespace GMD.PrivateMessenger.PL.API.Models.Message.Queries.GetList;

public class GetMessageQueryListHandler : 
    IRequestHandler<GetMessageListQuery, MessageListViewModel>, 
    IRequestHandler<GetMessageListByTextQuery, MessageListViewModel>
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
        int limit = request.Limit;
        int offset = request.Offset;
        Expression<Func<MessageDTO, bool>>? filter = null;
        IEnumerable<string>? includeProperties = new List<string>();

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
        int limit = request.Limit;
        int offset = request.Offset;
        Expression<Func<MessageDTO, bool>>? filter = dto => dto.Text.Contains(request.Text ?? "");
        IEnumerable<string>? includeProperties = new List<string>();

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
