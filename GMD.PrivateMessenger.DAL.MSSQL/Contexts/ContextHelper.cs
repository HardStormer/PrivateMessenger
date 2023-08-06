namespace GMD.PrivateMessenger.DAL.MSSQL.Contexts;

public static class ContextHelper
{
    public static void SetQueryFilters(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MessageDto>().HasQueryFilter(d => d.IsDeleted == false);
        modelBuilder.Entity<RoomDto>().HasQueryFilter(d => d.IsDeleted == false);
        modelBuilder.Entity<UserDto>().HasQueryFilter(d => d.IsDeleted == false);
    }
}
