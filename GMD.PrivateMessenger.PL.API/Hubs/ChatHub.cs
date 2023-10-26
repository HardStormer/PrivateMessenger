using GMD.PrivateMessenger.PL.API.Models.Message.Commands.Create;
using GMD.PrivateMessenger.PL.API.Models.Message.Queries;
using Microsoft.AspNetCore.SignalR;

namespace GMD.PrivateMessenger.PL.API.Hubs
{
    public class MessageHub : Hub
    {
        private readonly IMediator _mediator;
        protected IHubContext<MessageHub> _context;

        public MessageHub(IMediator mediator, IHubContext<MessageHub> context)
        {
            _mediator = mediator;
            _context = context;
        }

        public async Task<MessageViewModel> Add(CreateMessageCommand command)
        {
            var result = await _mediator.Send(command);
            await _context.Clients.All.SendAsync("ReceiveMessage");
            return result;
        }
    }
}