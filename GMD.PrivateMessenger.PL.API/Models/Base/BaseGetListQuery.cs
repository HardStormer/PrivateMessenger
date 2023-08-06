namespace GMD.PrivateMessenger.PL.API.Models.Base;

public abstract class BaseGetListQuery<TListViewModel, TViewModel> : BaseCommand, IRequest<TListViewModel>
    where TListViewModel : BaseListViewModel<TViewModel>
{
    public int Limit { get; set; } = 10;
    public int Offset { get; set; } = 0;
}
