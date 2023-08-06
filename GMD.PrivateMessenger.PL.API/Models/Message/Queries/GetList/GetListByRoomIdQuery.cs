namespace GMD.PrivateMessenger.PL.API.Models.Message.Queries.GetList;

public class GetMessageListByRoomIdQuery : BaseGetListQuery<MessageListViewModel, MessageViewModel>
{
    public Guid RoomId { get; set; }
}
