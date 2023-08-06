using GMD.PrivateMessenger.DAL.MSSQL.Base;

namespace GMD.PrivateMessenger.DAL.MSSQL;

public class RoomRepository : BaseRepository<RoomDto>, IRoomRepository
{
    public RoomRepository(IDbContextFactory<BaseDbContext> contextFactory) : base(contextFactory)
    {
    }

    public override async Task<RoomDto> AddAsync(RoomDto dto)
    {
        await using var context = await ContextFactory.CreateDbContextAsync();
        var dbSet = context.Rooms;

        foreach (var user in dto.Users)
        {
            context.Attach(user);
        }
        
        var result = (await dbSet.AddAsync(dto)).Entity;
        await context.SaveChangesAsync();

        var returnResult = await GetAsync(
            result.Id,
            new []{
                "Users"});
        
        return returnResult ?? result;
    }
}
