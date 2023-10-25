using GMD.PrivateMessenger.DAL.Interfaces;
using GMD.PrivateMessenger.PL.API.Authentication;

namespace GMD.PrivateMessenger.PL.API.Models.User.Queries.GetMyProfile;

public class GetMyProfileQueryHandler : IRequestHandler<GetMyProfileQuery, UserViewModel>
{
    private readonly IUserRepository _service;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _contextAccessor;

    public GetMyProfileQueryHandler(
        IUserRepository service,
        IMapper mapper, 
        IHttpContextAccessor contextAccessor)
    {
        _service = service;
        _mapper = mapper;
        _contextAccessor = contextAccessor;
    }

    public async Task<UserViewModel> Handle(GetMyProfileQuery request, CancellationToken cancellationToken)
    {
        var user = _contextAccessor.GetApiUser();

        if (user == null)
            throw new NotFoundException(nameof(UserDto), 1);

        var model = _mapper.Map<UserViewModel>(user);

        return model;
    }
}
