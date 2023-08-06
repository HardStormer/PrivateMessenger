using GMD.PrivateMessenger.PL.API.Models.Message.Commands.Create;
using GMD.PrivateMessenger.PL.API.Models.Message.Commands.Update;

namespace GMD.PrivateMessenger.PL.API.Models.Message.Queries;

public class MessageProfile : Profile
{
    public MessageProfile()
    {
        CreateMap<
            MessageDto,
            MessageViewModel>();
        CreateMap<
            MessageViewModel,
            UpdateMessageCommand>();
        CreateMap<
            UpdateMessageCommand,
            MessageDto>();
        CreateMap<
            CreateMessageCommand,
            MessageDto>();
    }
}
