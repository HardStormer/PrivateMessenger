namespace GMD.PrivateMessenger.PL.API.Models.Room.Queries.Get;

public class GetRoomQuery : IRequest<RoomViewModel>
{
    public Guid Id { get; set; }
}
