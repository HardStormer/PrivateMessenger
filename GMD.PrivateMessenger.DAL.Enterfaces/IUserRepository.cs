namespace GMD.PrivateMessenger.DAL.Interfaces;

public interface IUserRepository : IBaseRepository<UserDto>
{
    Task<UserDto?> GetAsync(string login);
}
