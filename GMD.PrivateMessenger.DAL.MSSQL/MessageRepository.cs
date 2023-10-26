using GMD.PrivateMessenger.Common.SignalR;
using GMD.PrivateMessenger.DAL.MSSQL.Base;

namespace GMD.PrivateMessenger.DAL.MSSQL;

public class MessageRepository : BaseRepository<MessageDto>, IMessageRepository
{
    public MessageRepository(IDbContextFactory<BaseDbContext> contextFactory) : base(contextFactory)
    {
    }
    
    public event EventHandler<SignalRNotifierEventAgrs<MessageDto>>? Notifier = delegate{};
    
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
        
        Notifier?.Invoke(this, new SignalRNotifierEventAgrs<MessageDto>(returnResult ?? result, dto.RoomId));

        return returnResult ?? result;
    }
    
    public async Task<int> CountNewMessages(Guid roomId)
    {
        await using var context = await ContextFactory.CreateDbContextAsync();
        
        return await context.Messages
            .Include(r => r.User)
            .AsNoTracking()
            .CountAsync(r=>r.IsRead == false && r.RoomId == roomId);
    }
}
