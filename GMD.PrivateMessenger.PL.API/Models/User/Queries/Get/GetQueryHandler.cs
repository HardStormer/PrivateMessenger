using GMD.PrivateMessenger.DAL.Interfaces;

namespace GMD.PrivateMessenger.PL.API.Models.User.Queries.Get;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserViewModel>
{
    private readonly IUserRepository _service;
    private readonly IMapper _mapper;

    public GetUserQueryHandler(
        IUserRepository service,
        IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public async Task<UserViewModel> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var entity = await _service.GetAsync(request.Id);

        if (entity == null)
            throw new NotFoundException(nameof(UserDto), request.Id);

        var model = _mapper.Map<UserViewModel>(entity);

        return model;
    }
}
