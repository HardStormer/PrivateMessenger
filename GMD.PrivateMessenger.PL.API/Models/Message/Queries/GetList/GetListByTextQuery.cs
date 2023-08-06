namespace GMD.PrivateMessenger.PL.API.Models.Message.Queries.GetList;

public class GetMessageListByTextQuery : BaseGetListQuery<MessageListViewModel, MessageViewModel>
{
    public string Text { get; set; } = string.Empty;
}
