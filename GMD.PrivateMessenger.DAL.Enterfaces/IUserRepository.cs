namespace GMD.PrivateMessenger.DAL.Interfaces;

public interface IUserRepository : IBaseRepository<UserDTO>
{
    Task<UserDTO?> GetAsync(string login);
}
