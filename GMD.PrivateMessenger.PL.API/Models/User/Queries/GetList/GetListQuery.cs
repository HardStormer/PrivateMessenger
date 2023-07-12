using System.Linq.Expressions;

namespace GMD.PrivateMessenger.PL.API.Models.User.Queries.GetList;

public class GetUserListQuery : IRequest<UserListViewModel>
{
    public int Limit { get; set; } = 10;
    public int Offset { get; set; } = 0;
}
