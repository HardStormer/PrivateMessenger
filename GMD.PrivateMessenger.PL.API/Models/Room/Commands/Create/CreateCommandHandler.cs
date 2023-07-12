using GMD.PrivateMessenger.DAL.Interfaces;

namespace GMD.PrivateMessenger.PL.API.Models.Room.Commands.Create;

public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, Guid>
{
    private readonly IRoomRepository _service;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateRoomCommand> _validator;

    public CreateRoomCommandHandler(
        IRoomRepository service,
        IMapper mapper,
        IValidator<CreateRoomCommand> validator)
    {
        _service = service;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Guid> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);
        var DTO = _mapper.Map<RoomDTO>(request);
        var result = await _service.AddAsync(DTO);

        return result.Id;
    }
}
