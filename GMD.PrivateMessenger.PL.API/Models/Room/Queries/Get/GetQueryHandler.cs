using GMD.PrivateMessenger.DAL.Interfaces;

namespace GMD.PrivateMessenger.PL.API.Models.Room.Queries.Get;

public class GetRoomQueryHandler : IRequestHandler<GetRoomQuery, RoomViewModel>
{
    private readonly IRoomRepository _service;
    private readonly IMapper _mapper;

    public GetRoomQueryHandler(
        IRoomRepository service,
        IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public async Task<RoomViewModel> Handle(GetRoomQuery request, CancellationToken cancellationToken)
    {
        var entity = await _service.GetAsync(request.Id, new []{"Users"});

        if (entity == null)
            throw new NotFoundException(nameof(RoomDto), request.Id);

        var model = _mapper.Map<RoomViewModel>(entity);

        return model;
    }
}
