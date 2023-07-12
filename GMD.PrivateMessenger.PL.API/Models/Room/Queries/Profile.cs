using GMD.PrivateMessenger.PL.API.Models.Room.Commands.Create;
using GMD.PrivateMessenger.PL.API.Models.Room.Commands.Update;

namespace GMD.PrivateMessenger.PL.API.Models.Room.Queries;

public class RoomProfile : Profile
{
    public RoomProfile()
    {
        CreateMap<
            RoomDTO,
            RoomViewModel>();
        CreateMap<
            RoomViewModel,
            UpdateRoomCommand>();
        CreateMap<
            UpdateRoomCommand,
            RoomDTO>();
        CreateMap<
            CreateRoomCommand,
            RoomDTO>();
    }
}
