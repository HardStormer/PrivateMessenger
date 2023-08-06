namespace GMD.PrivateMessenger.PL.API.Models.Base;

public abstract class BaseListViewModel<TModel>
{
    public IEnumerable<TModel> ModelList { get; set; } = Enumerable.Empty<TModel>();
    public int TotalCount { get; set; }
}
