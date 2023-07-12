using System.Linq.Expressions;
using GMD.PrivateMessenger.DAL.Interfaces;

namespace GMD.PrivateMessenger.PL.API.Models.Room.Queries.GetList;

public class GetRoomQueryListHandler : 
    IRequestHandler<GetRoomListQuery, RoomListViewModel>, 
    IRequestHandler<GetRoomListByNameQuery, RoomListViewModel>
{
    private readonly IRoomRepository _service;
    private readonly IMapper _mapper;

    public GetRoomQueryListHandler(
        IRoomRepository service,
        IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public async Task<RoomListViewModel> Handle(GetRoomListQuery request, CancellationToken cancellationToken)
    {
        int limit = request.Limit;
        int offset = request.Offset;
        Expression<Func<RoomDTO, bool>>? filter = null;
        IEnumerable<string>? includeProperties = new List<string>();

        var wrapper = await _service.GetAsync(limit, offset, filter, includeProperties);

        var entities = wrapper.Items;

        var models = _mapper.Map<IEnumerable<RoomViewModel>>(entities);

        var listView = new RoomListViewModel
        {
            ModelList = models,
            TotalCount = wrapper.TotalCount
        };

        return listView;
    }
    public async Task<RoomListViewModel> Handle(GetRoomListByNameQuery request, CancellationToken cancellationToken)
    {
        int limit = request.Limit;
        int offset = request.Offset;
        Expression<Func<RoomDTO, bool>>? filter = dto => dto.Name != null && dto.Name.Contains(request.Name ?? "");
        IEnumerable<string>? includeProperties = new List<string>();

        var wrapper = await _service.GetAsync(limit, offset, filter, includeProperties);

        var entities = wrapper.Items;

        var models = _mapper.Map<IEnumerable<RoomViewModel>>(entities);

        var listView = new RoomListViewModel
        {
            ModelList = models,
            TotalCount = wrapper.TotalCount
        };

        return listView;
    }
}
