using GMD.PrivateMessenger.DAL.MSSQL.Base;

namespace GMD.PrivateMessenger.DAL.MSSQL;

public class MessageRepository : BaseRepository<MessageDto>, IMessageRepository
{
    public MessageRepository(IDbContextFactory<BaseDbContext> contextFactory) : base(contextFactory)
    {
    }
    public override async Task<MessageDto> AddAsync(MessageDto dto)
    {
        await using var context = await ContextFactory.CreateDbContextAsync();
        var dbSet = context.Messages;

        context.Attach(dto.User);
        
        var result = (await dbSet.AddAsync(dto)).Entity;
        await context.SaveChangesAsync();

        var returnResult = await GetAsync(
            result.Id,
            new []{
                "User"});
        
        return returnResult ?? result;
    }
}
