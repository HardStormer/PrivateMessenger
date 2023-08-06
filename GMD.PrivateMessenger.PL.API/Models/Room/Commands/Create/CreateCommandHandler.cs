using GMD.PrivateMessenger.DAL.Interfaces;
using GMD.PrivateMessenger.PL.API.Authentication;
using GMD.PrivateMessenger.PL.API.Models.Room.Queries;

namespace GMD.PrivateMessenger.PL.API.Models.Room.Commands.Create;

public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, RoomViewModel>
{
    private readonly IRoomRepository _service;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateRoomCommand> _validator;
    private readonly IHttpContextAccessor _contextAccessor;

    public CreateRoomCommandHandler(
        IRoomRepository service,
        IMapper mapper,
        IValidator<CreateRoomCommand> validator,
        IHttpContextAccessor httpContextAccessor)
    {
        _service = service;
        _mapper = mapper;
        _validator = validator;
        _contextAccessor = httpContextAccessor;
    }

    public async Task<RoomViewModel> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        var user = _contextAccessor.GetApiUser();

        if (user == null)
            throw new ExpectedException("User not found", HttpStatusCode.Forbidden);

        var dto = _mapper.Map<RoomDto>(request);

        dto.Id = Guid.NewGuid();
        dto.Users.Add(user);
        
        var resultDto = await _service.AddAsync(dto);
        
        var result = _mapper.Map<RoomViewModel>(await _service.GetAsync(resultDto.Id));
        return result;
    }
}
