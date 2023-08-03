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
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserDTO>()
            .HasMany(e => e.Rooms)
            .WithMany(e => e.Users);
        modelBuilder.Entity<UserDTO>()
            .HasMany(e => e.Messages)
            .WithOne().HasForeignKey(e => e.UserId);

        modelBuilder.Entity<RoomDTO>()
            .HasMany(e => e.Messages)
            .WithOne().HasForeignKey(e => e.RoomId);
	}

    public DbSet<MessageDTO> Messages { get; set; }
    public DbSet<UserDTO> Users { get; set; }
    public DbSet<RoomDTO> Rooms { get; set; }
}
