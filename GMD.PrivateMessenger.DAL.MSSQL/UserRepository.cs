namespace GMD.PrivateMessenger.DAL.MSSQL;

public class UserRepository : BaseRepository<UserDTO>, IUserRepository
{
    public UserRepository(IDbContextFactory<BaseDbContext> contextFactory) : base(contextFactory)
    {
    }

    public async Task<UserDTO?> GetAsync(string login)
    {
        await using BaseDbContext context = await _contextFactory.CreateDbContextAsync();

        var gets = await context.Set<UserDTO>().Where(x => x.Login == login).FirstOrDefaultAsync();

        return gets;
    }
}
