using GMD.PrivateMessenger.DAL.MSSQL.Base;

namespace GMD.PrivateMessenger.DAL.MSSQL;

public class UserRepository : BaseRepository<UserDto>, IUserRepository
{
    public UserRepository(IDbContextFactory<BaseDbContext> contextFactory) : base(contextFactory)
    {
    }

    public async Task<UserDto?> GetAsync(string login)
    {
        await using BaseDbContext context = await ContextFactory.CreateDbContextAsync();

        var gets = await context.Set<UserDto>().Where(x => x.Login == login).FirstOrDefaultAsync();

        return gets;
    }
}
