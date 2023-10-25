using System.Linq.Expressions;
using GMD.PrivateMessenger.DAL.Interfaces;
using GMD.PrivateMessenger.PL.API.Authentication;

namespace GMD.PrivateMessenger.PL.API.Models.Room.Queries.GetList;

public class GetRoomQueryListHandler : 
    IRequestHandler<GetRoomListQuery, RoomListViewModel>, 
    IRequestHandler<GetRoomListByNameQuery, RoomListViewModel>,
    IRequestHandler<GetRoomListByUserIdQuery, RoomListViewModel>,
    IRequestHandler<GetRoomListMyQuery, RoomListViewModel>
{
    private readonly IRoomRepository _service;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetRoomQueryListHandler(
        IRoomRepository service,
        IMapper mapper, 
        IHttpContextAccessor contextAccessor)
    {
        _service = service;
        _mapper = mapper;
        _contextAccessor = contextAccessor;
    }

    public async Task<RoomListViewModel> Handle(GetRoomListQuery request, CancellationToken cancellationToken)
    {
        var limit = request.Limit;
        var offset = request.Offset;
        Expression<Func<RoomDto, bool>>? filter = null;
        var includeProperties = new[]
        {
            "Users"
        };

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
        var limit = request.Limit;
        var offset = request.Offset;
        Expression<Func<RoomDto, bool>> filter = dto => dto.Name != null && dto.Name.Contains(request.Name);
        var includeProperties = new[]
        {
            "Users"
        };

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
    
    public async Task<RoomListViewModel> Handle(GetRoomListByUserIdQuery request, CancellationToken cancellationToken)
    {
        var limit = request.Limit;
        var offset = request.Offset;
        Expression<Func<RoomDto, bool>> filter = dto => dto.Users.Count > 0 && dto.Users.Any(x => x.Id == request.UserId);
        var includeProperties = new[]
        {
            "Users"
        };

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

    public async Task<RoomListViewModel> Handle(GetRoomListMyQuery request, CancellationToken cancellationToken)
    {
        var userId = _contextAccessor.GetApiUserId();
        
        var limit = request.Limit;
        var offset = request.Offset;
        Expression<Func<RoomDto, bool>> filter = dto => dto.Users.Count > 0 && dto.Users.Any(x => x.Id == userId);
        var includeProperties = new[]
        {
            "Users"
        };

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
