using GMD.PrivateMessenger.PL.API.Models.Room.Commands.Create;
using GMD.PrivateMessenger.PL.API.Models.Room.Commands.Update;

namespace GMD.PrivateMessenger.PL.API.Models.Room.Queries;

public class RoomProfile : Profile
{
    public RoomProfile()
    {
        CreateMap<
            RoomDto,
            RoomViewModel>();
        CreateMap<
            RoomViewModel,
            UpdateRoomCommand>();
        CreateMap<
            UpdateRoomCommand,
            RoomDto>();
        CreateMap<
            CreateRoomCommand,
            RoomDto>();
    }
}
