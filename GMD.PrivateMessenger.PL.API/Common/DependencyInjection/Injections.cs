using GMD.PrivateMessenger.DAL.Interfaces;
using GMD.PrivateMessenger.DAL.MSSQL;
using GMD.PrivateMessenger.PL.API.Hubs;

namespace GMD.PrivateMessenger.PL.API.Common.DependencyInjection;
/// <summary>
/// Статический класс Injections содержит методы для внедрения зависимостей (Dependency Injection) репозиториев в контейнер служб IServiceCollection.
/// </summary>
public static class Injections
{
    /// <summary>
    /// Расширяющий метод для IServiceCollection, который регистрирует репозитории в контейнере служб.
    /// </summary>
    /// <param name="services"></param>
    public static void InjectRepositories(this IServiceCollection services)
    {
        services.AddScoped<
            IMessageRepository,
            MessageRepository>();
        services.AddScoped<
            IUserRepository,
            UserRepository>();
        services.AddScoped<
            IRoomRepository,
            RoomRepository>();
        
        services.AddScoped<MessageHub>();
    }
}
