namespace GMD.PrivateMessenger.DAL.MSSQL.Contexts;

public static class ContextHelper
{
    public static void SetQueryFilters(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MessageDTO>().HasQueryFilter(d => d.IsDeleted == false);
    }
}
