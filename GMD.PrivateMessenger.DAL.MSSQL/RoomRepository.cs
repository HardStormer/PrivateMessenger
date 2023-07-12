namespace GMD.PrivateMessenger.DAL.MSSQL;

public class RoomRepository : BaseRepository<RoomDTO>, IRoomRepository
{
    public RoomRepository(IDbContextFactory<BaseDbContext> contextFactory) : base(contextFactory)
    {
    }
}
