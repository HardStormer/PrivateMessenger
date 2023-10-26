// using GMD.PrivateMessenger.Common.SignalR;
// using GMD.PrivateMessenger.Common.SignalR.Models;
// using GMD.PrivateMessenger.DAL.Interfaces;
// using GMD.PrivateMessenger.PL.API.Authentication;
// using GMD.PrivateMessenger.PL.API.Models.Message.Commands.Create;
// using GMD.PrivateMessenger.PL.API.Models.Message.Queries;
// using Microsoft.AspNetCore.SignalR;
//
// namespace GMD.PrivateMessenger.PL.API.Hubs
// {
//     [Authorize]
//     public class MessageHub : Hub
//     {
//         private readonly IMessageRepository _messageRepository;
//         private readonly IHubContext<MessageHub> _context;
//         private readonly IMapper _mapper;
//         private readonly IMediator _mediator;
//
//         public MessageHub(IMessageRepository messageRepository, IHubContext<MessageHub> context, IMapper mapper, IMediator mediator)
//         {
//             _messageRepository = messageRepository;
//             _context = context;
//             _mapper = mapper;
//             _mediator = mediator;
//
//             _messageRepository.Notifier += MessageServiceOnNotifier;
//         }
//
//         private async void MessageServiceOnNotifier(object? sender, SignalRNotifierEventAgrs<MessageDto> e)
//         {
//             if(string.IsNullOrWhiteSpace(e?.UserId.ToString())) return;
//
//             if (e.Payload == null) return;
//             
//             var newMsg = await _messageRepository.CountNewMessages(e.Payload.RoomId);
//             await _context.Clients?.Groups(e?.UserId.ToString() ?? string.Empty)?.SendAsync("MessageInfo",
//                 JsonConvert.SerializeObject(new SignalRNotifierEventAgrs<MessageInfoWrapper>(new MessageInfoWrapper(e.Payload,newMsg),e.UserId), Formatting.Indented, new JsonSerializerSettings
//                 {
//                     ReferenceLoopHandling = ReferenceLoopHandling.Ignore
//                 }));
//         }
//
//         public async Task<MessageViewModel> Add(CreateMessageCommand command)
//         {
//             return await _mediator.Send(command);
//         }
//
//         public override Task OnConnectedAsync()
//         {
//             var contextUser = Context.User;
//             if (contextUser == null) return base.OnConnectedAsync();
//             if (contextUser.Identity is not ApiUserIdentity apiUser) return base.OnConnectedAsync();
//             
//             var id = apiUser.UserData.Id;
//             _context.Groups.AddToGroupAsync(Context.ConnectionId, id.ToString());
//             return base.OnConnectedAsync();
//         }
//         
//     }
// }
