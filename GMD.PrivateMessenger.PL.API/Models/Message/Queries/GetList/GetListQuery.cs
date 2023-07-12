using System.Linq.Expressions;

namespace GMD.PrivateMessenger.PL.API.Models.Message.Queries.GetList;

public class GetMessageListQuery : IRequest<MessageListViewModel>
{
    public int Limit { get; set; } = 10;
    public int Offset { get; set; } = 0;
}
