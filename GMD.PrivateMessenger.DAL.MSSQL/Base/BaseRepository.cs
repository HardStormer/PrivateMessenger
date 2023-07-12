using GMD.PrivateMessenger.DAL.Interfaces;

namespace SpaceApp.Vodnik.DAL.SQL.Base;

public abstract class BaseRepository<TRepository> : IBaseRepository<TRepository>
    where TRepository : BaseDTO
{
    protected readonly IDbContextFactory<BaseDbContext> _contextFactory;

    public BaseRepository(IDbContextFactory<BaseDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public virtual async Task<TRepository> AddAsync(TRepository dto)
    {
        await using BaseDbContext context = await _contextFactory.CreateDbContextAsync();

        var result = await context.AddAsync(dto);
        await context.SaveChangesAsync();

        return result.Entity;
    }

    public virtual async Task<TRepository?> EditAsync(TRepository dto)
    {
        await using BaseDbContext context = await _contextFactory.CreateDbContextAsync();

        if (dto != null)
        {
            var old = await context.Set<TRepository>().FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (old == null)
            {
                return await AddAsync(dto);
            }

            context.Entry(old).CurrentValues.SetValues(dto);

            await context.SaveChangesAsync();

            return old;
        }
        else return null;
    }

    public virtual async Task<IEnumerable<TRepository>?> GetAllAsync()
    {
        await using BaseDbContext context = await _contextFactory.CreateDbContextAsync();

        var gets = await context.Set<TRepository>().ToListAsync();

        return gets;
    }

    public virtual async Task<TRepository?> GetAsync(Guid id)
    {
        await using BaseDbContext context = await _contextFactory.CreateDbContextAsync();

        var gets = await context.FindAsync<TRepository>(id);

        return gets;
    }

    public async Task<GetWrapper<IEnumerable<TRepository>>> GetAsync(int? limit = null, int? offset = null, Expression<Func<TRepository, bool>>? filter = null, IEnumerable<string>? includeProperties = null)
    {
        await using BaseDbContext context = await _contextFactory.CreateDbContextAsync();

        IQueryable<TRepository> query = context.Set<TRepository>().AsQueryable();

        if (includeProperties != null)
        {
            foreach (var includeName in includeProperties)
            {
                query = query.Include(includeName);
            }

        }

        query = query.OrderByDescending(r => r.CreatedAt);


        if (filter != null)
        {
            var dalFilterExpression = filter;
            query = query
                .Where(dalFilterExpression);
        }

        var count = await query.CountAsync();

        if (limit != null)
        {
            query = query.Take(limit.Value);
        }

        if (offset != null)
        {
            query = query.Skip(offset.Value);
        }

        var items = await query.ToListAsync();



        return new GetWrapper<IEnumerable<TRepository>>(items, count);
    }

    public virtual async Task<TRepository?> PatchAsync(TRepository dto)
    {
        await using DbContext context = await _contextFactory.CreateDbContextAsync();

        if (dto == null)
        {
            return null;
        }
        var old = await context.Set<TRepository>().FirstOrDefaultAsync(x => x.Id == dto.Id);

        if (old == null)
        {
            return await AddAsync(dto);
        }
        var propertiesOld = old.GetType().GetProperties();

        dto.GetType().GetProperties().ToList().ForEach(prop =>
        {
            if (prop.GetValue(dto) == null || (prop.GetValue(dto)!.GetType() == typeof(int) && (int)prop.GetValue(dto)! == 0))
            {
                prop.SetValue(dto, propertiesOld.First(p => p.Name == prop.Name).GetValue(old));
            }
        });

        context.Entry(old).CurrentValues.SetValues(dto);

        await context.SaveChangesAsync();

        return await context.Set<TRepository>()
            .FirstOrDefaultAsync(x => x.Id == dto.Id);
    }

    public virtual async Task RemoveAsync(Guid id, bool soft = false)
    {
        await using BaseDbContext context = await _contextFactory.CreateDbContextAsync();

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
