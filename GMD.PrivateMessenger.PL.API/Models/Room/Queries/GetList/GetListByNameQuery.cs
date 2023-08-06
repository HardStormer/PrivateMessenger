namespace GMD.PrivateMessenger.PL.API.Models.Room.Queries.GetList;

public class GetRoomListByNameQuery : BaseGetListQuery<RoomListViewModel, RoomViewModel>
{
    public string Name { get; set; } = string.Empty;
}
