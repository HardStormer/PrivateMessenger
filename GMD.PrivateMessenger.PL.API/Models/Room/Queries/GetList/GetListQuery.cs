using System.Linq.Expressions;

namespace GMD.PrivateMessenger.PL.API.Models.Room.Queries.GetList;

public class GetRoomListQuery : IRequest<RoomListViewModel>
{
    public int Limit { get; set; } = 10;
    public int Offset { get; set; } = 0;
}
