using GMD.PrivateMessenger.DAL.Interfaces;

namespace GMD.PrivateMessenger.PL.API.Models.Room.Commands.Update;

public class UpdateRoomCommandHandler :
    IRequestHandler<UpdateRoomCommand>
{
    private readonly IRoomRepository _service;
    private readonly IMapper _mapper;
    private readonly IValidator<UpdateRoomCommand> _validator;

    public UpdateRoomCommandHandler(
        IRoomRepository service,
        IMapper mapper,
        IValidator<UpdateRoomCommand> validator)
    {
        _service = service;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);

        var DTO = _mapper.Map<RoomDTO>(request);

        await _service.PatchAsync(DTO);
    }
}
