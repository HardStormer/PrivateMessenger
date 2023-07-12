namespace GMD.PrivateMessenger.DAL.MSSQL.Contexts;

public class BaseDbContext : DbContext
{
    public BaseDbContext(DbContextOptions<BaseDbContext> options)
        : base(options)
    {
        Database.Migrate();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<MessageDTO> Messages { get; set; }
    public DbSet<UserDTO> Users { get; set; }
    public DbSet<RoomDTO> Rooms { get; set; }
}
