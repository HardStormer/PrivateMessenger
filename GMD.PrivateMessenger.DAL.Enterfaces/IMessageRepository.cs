using GMD.PrivateMessenger.Common.SignalR;

namespace GMD.PrivateMessenger.DAL.Interfaces;

public interface IMessageRepository : IBaseRepository<MessageDto>
{
    public Task<int> CountNewMessages(Guid roomId);
    public event EventHandler<SignalRNotifierEventAgrs<MessageDto>>? Notifier;
}
