using System.Linq.Expressions;
using GMD.PrivateMessenger.DAL.Interfaces;

namespace GMD.PrivateMessenger.PL.API.Models.User.Queries.GetList;

public class GetUserQueryListHandler : 
    IRequestHandler<GetUserListQuery, UserListViewModel>
{
    private readonly IUserRepository _service;
    private readonly IMapper _mapper;

    public GetUserQueryListHandler(
        IUserRepository service,
        IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public async Task<UserListViewModel> Handle(GetUserListQuery request, CancellationToken cancellationToken)
    {
        int limit = request.Limit;
        int offset = request.Offset;
        Expression<Func<UserDTO, bool>>? filter = null;
        IEnumerable<string>? includeProperties = new List<string>();

        var wrapper = await _service.GetAsync(limit, offset, filter, includeProperties);

        var entities = wrapper.Items;

        var models = _mapper.Map<IEnumerable<UserViewModel>>(entities);

        var listView = new UserListViewModel
        {
            ModelList = models,
            TotalCount = wrapper.TotalCount
        };

        return listView;
    }
}
