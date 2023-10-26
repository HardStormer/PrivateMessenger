namespace GMD.PrivateMessenger.Common.SignalR;

public class SignalRNotifierEventAgrs<T> : System.EventArgs where T : class
{
    public T? Payload { get; set; }
    
    public Guid UserId { get; set; }

    public SignalRNotifierEventAgrs(T? payload, Guid userId)
    {
        Payload = payload;
        UserId = userId;
    }
}