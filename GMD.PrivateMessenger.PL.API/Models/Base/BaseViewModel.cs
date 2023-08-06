namespace GMD.PrivateMessenger.PL.API.Models.Base;

public abstract class BaseViewModel
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    //public bool IsDeleted { get; set; }
}
