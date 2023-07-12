using GMD.PrivateMessenger.PL.API.Models.User.Commands.Register;
using GMD.PrivateMessenger.PL.API.Models.User.Commands.Update;

namespace GMD.PrivateMessenger.PL.API.Models.User.Queries;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<
            UserDTO,
            UserViewModel>();
        CreateMap<
            RegisterUserCommand,
            UserDTO>();
    }
}
