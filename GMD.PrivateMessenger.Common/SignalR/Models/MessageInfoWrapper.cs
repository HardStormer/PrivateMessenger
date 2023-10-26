using GMD.PrivateMessenger.DAL.Entities;

namespace GMD.PrivateMessenger.Common.SignalR.Models;

public class MessageInfoWrapper
{
    public MessageInfoWrapper(MessageDto? message, int newMessages)
    {
        Message = message;
        NewMessages = newMessages;
    }

    public MessageDto? Message { get; set; }
    
    public int NewMessages { get; set; }
}