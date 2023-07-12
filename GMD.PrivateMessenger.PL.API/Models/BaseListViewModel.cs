namespace GMD.PrivateMessenger.PL.API.Models;

public class BaseListViewModel<TModel>
{
    public IEnumerable<TModel> ModelList { get; set; } = new List<TModel>();
    public int TotalCount { get; set; }
}
