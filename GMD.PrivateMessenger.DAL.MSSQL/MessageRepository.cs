namespace GMD.PrivateMessenger.DAL.MSSQL;

public class MessageRepository : BaseRepository<MessageDTO>, IMessageRepository
{
    public MessageRepository(IDbContextFactory<BaseDbContext> contextFactory) : base(contextFactory)
    {
    }
}
