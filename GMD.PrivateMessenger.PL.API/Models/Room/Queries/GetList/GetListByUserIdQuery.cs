namespace GMD.PrivateMessenger.PL.API.Models.Room.Queries.GetList;

public class GetRoomListByUserIdQuery : BaseGetListQuery<RoomListViewModel, RoomViewModel>
{
    public Guid UserId { get; set; }
}
