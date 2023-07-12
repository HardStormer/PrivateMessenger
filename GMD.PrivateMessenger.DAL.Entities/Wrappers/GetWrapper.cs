namespace GMD.PrivateMessenger.DAL.Entities.Wrappers;

public class GetWrapper<TEnumerable> where TEnumerable : class, IEnumerable
{
    public GetWrapper(TEnumerable? items, int totalCount)
    {
        Items = items;
        TotalCount = totalCount;
    }

    public TEnumerable? Items { get; set; }

    public int TotalCount { get; set; }
}
