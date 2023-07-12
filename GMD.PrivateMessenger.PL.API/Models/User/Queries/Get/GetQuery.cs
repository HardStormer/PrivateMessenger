namespace GMD.PrivateMessenger.PL.API.Models.User.Queries.Get;

public class GetUserQuery : IRequest<UserViewModel>
{
    public Guid Id { get; set; }
}
