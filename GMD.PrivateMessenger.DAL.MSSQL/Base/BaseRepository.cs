namespace GMD.PrivateMessenger.DAL.MSSQL.Base;

public abstract class BaseRepository<TRepository> : IBaseRepository<TRepository>
    where TRepository : BaseDto
{
    protected readonly IDbContextFactory<BaseDbContext> ContextFactory;

    public BaseRepository(IDbContextFactory<BaseDbContext> contextFactory)
    {
        ContextFactory = contextFactory;
    }

    public virtual async Task<TRepository> AddAsync(TRepository dto)
    {
        await using var context = await ContextFactory.CreateDbContextAsync();
        var dbSet = context.Set<TRepository>();
        
        var result = await dbSet.AddAsync(dto);
        await context.SaveChangesAsync();

        return result.Entity;
    }

    public virtual async Task<TRepository?> EditAsync(TRepository dto)
    {
        await using var context = await ContextFactory.CreateDbContextAsync();
        var dbSet = context.Set<TRepository>();

        var old = await dbSet
            .FirstOrDefaultAsync(x => x.Id == dto.Id);

        if (old == null)
        {
            return await AddAsync(dto);
        }

        context.Entry(old).CurrentValues.SetValues(dto);

        await context.SaveChangesAsync();

        return old;
        
    }

    public virtual async Task<IEnumerable<TRepository>> GetAllAsync()
    {
        await using var context = await ContextFactory.CreateDbContextAsync();
        var dbSet = context.Set<TRepository>();

        var gets = await dbSet.ToListAsync();

        return gets;
    }

    public virtual async Task<TRepository?> GetAsync(
        Guid id,
        IEnumerable<string>? includeProperties = null)
    {
        await using var context = await ContextFactory.CreateDbContextAsync();
        var dbSet = context.Set<TRepository>();

        var query = dbSet.AsQueryable();

        if (includeProperties != null)
        {
            foreach (var includeName in includeProperties)
            {
                query = query
                    .Include(includeName);
            }
        }

        var gets = await query.FirstOrDefaultAsync(x => x.Id == id);

        return gets;
    }

    public async Task<GetWrapper<IEnumerable<TRepository>>> GetAsync(
        int? limit = null, 
        int? offset = null, 
        Expression<Func<TRepository, bool>>? filter = null, 
        IEnumerable<string>? includeProperties = null)
    {
        await using var context = await ContextFactory.CreateDbContextAsync();
        var dbSet = context.Set<TRepository>();

        var query = dbSet.AsQueryable();

        if (includeProperties != null)
        {
            foreach (var includeName in includeProperties)
            {
                query = query
                    .Include(includeName);
            }
        }

        query = query.OrderByDescending(r => r.CreatedAt);
        
        if (filter != null)
        {
            query = query
                .Where(filter);
        }

        var count = await query.CountAsync();
        
        if (offset != null)
        {
            query = query
                .Skip(offset.Value);
        }
        
        if (limit != null)
        {
            query = query
                .Take(limit.Value);
        }

        var items = await query.ToListAsync();

        return new GetWrapper<IEnumerable<TRepository>>(items, count);
    }

    public virtual async Task<TRepository?> PatchAsync(TRepository dto)
    {
        await using var context = await ContextFactory.CreateDbContextAsync();
        var dbSet = context.Set<TRepository>();

        var old = await dbSet.FirstOrDefaultAsync(x => x.Id == dto.Id);

        if (old == null)
        {
            return await AddAsync(dto);
        }
        var propertiesOld = old.GetType().GetProperties();

        dto.GetType().GetProperties().ToList().ForEach(prop =>
        {
            if (
                prop.GetValue(dto) == null 
                || 
                (prop.GetValue(dto) is int && (int)prop.GetValue(dto)! == 0)
                )
            {
                prop.SetValue(dto, propertiesOld.First(p => p.Name == prop.Name).GetValue(old));
            }
        });

        context.Entry(old).CurrentValues.SetValues(dto);

        await context.SaveChangesAsync();

        return await dbSet
            .FirstOrDefaultAsync(x => x.Id == dto.Id);
    }

    public virtual async Task RemoveAsync(Guid id, bool soft = false)
    {
        await using var context = await ContextFactory.CreateDbContextAsync();

        var removable = await context
            .FindAsync<TRepository>(id);

        if (removable != null)
        {
            switch (soft)
            {
                case true:
                    removable.IsDeleted = true;
                    break;
                case false:
                    context.Remove(removable);
                    break;
            }

        }

        await context.SaveChangesAsync();
    }
}
