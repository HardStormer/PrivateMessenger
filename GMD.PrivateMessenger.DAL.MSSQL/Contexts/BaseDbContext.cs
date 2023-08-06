namespace GMD.PrivateMessenger.DAL.MSSQL.Contexts;

public sealed class BaseDbContext : DbContext
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
        modelBuilder.Entity<UserDto>()
            .HasMany(e => e.Rooms)
            .WithMany(e => e.Users);
        
        modelBuilder.Entity<UserDto>()
            .HasMany(e => e.Messages)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId);

        modelBuilder.Entity<RoomDto>()
            .HasMany(e => e.Messages)
            .WithOne()
            .HasForeignKey(e => e.RoomId);
	}

    public DbSet<MessageDto> Messages { get; set; }
    public DbSet<UserDto> Users { get; set; }
    public DbSet<RoomDto> Rooms { get; set; }
}
