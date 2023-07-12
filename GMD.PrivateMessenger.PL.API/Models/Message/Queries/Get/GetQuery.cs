namespace GMD.PrivateMessenger.PL.API.Models.Message.Queries.Get;

public class GetMessageQuery : IRequest<MessageViewModel>
{
    public Guid Id { get; set; }
}
