using GMD.PrivateMessenger.DAL.MSSQL.Base;

namespace GMD.PrivateMessenger.DAL.MSSQL;

public class MessageRepository : BaseRepository<MessageDto>, IMessageRepository
{
    public MessageRepository(IDbContextFactory<BaseDbContext> contextFactory) : base(contextFactory)
    {
    }
}
