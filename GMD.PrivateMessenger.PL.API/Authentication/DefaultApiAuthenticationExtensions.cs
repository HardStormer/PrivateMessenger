using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.SignalR;

namespace GMD.PrivateMessenger.PL.API.Authentication
{
    /// <summary>
    /// Статический класс DefaultApiAuthenticationExtensions содержит методы-расширения для настройки и использования аутентификации в API.
    /// </summary>
    public static class DefaultApiAuthenticationExtensions
    {
        /// <summary>
        /// Метод-расширение для AuthenticationBuilder, который добавляет аутентификацию по умолчанию в конвейер аутентификации приложения. Он регистрирует схему аутентификации с помощью DefaultApiAuthenticationOptions и DefaultApiAuthenticationHandler. Вы можете настроить параметры аутентификации, передавая делегат configureOptions для настройки опций аутентификации.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configureOptions"></param>
        /// <returns></returns>
        public static AuthenticationBuilder AddDefaultApiAuthentication(this AuthenticationBuilder builder, Action<DefaultApiAuthenticationOptions>? configureOptions = null)
        {
            return builder.AddScheme<DefaultApiAuthenticationOptions, DefaultApiAuthenticationHandler>(DefaultApiAuthenticationOptions.DefaultScheme, configureOptions ?? (options =>
            {
            }));
        }
        /// <summary>
        /// Метод-расширение для ControllerBase, который возвращает объект UserDTO, представляющий пользователя, аутентифицированного в контексте текущего запроса. Этот метод извлекает данные о пользователе из свойства User.Identity в виде ApiUserIdentity и возвращает связанный объект UserDTO. Если пользователь не аутентифицирован или данные о пользователе недоступны, метод вернет значение null.
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static UserDto? GetApiUser(this ControllerBase controller)
        {
            var userData = (controller?.User?.Identity as ApiUserIdentity)?.UserData;
            return userData;
        }
        /// <summary>
        /// Метод-расширение для IHttpContextAccessor, который возвращает объект UserDTO, представляющий пользователя, аутентифицированного в контексте текущего запроса. Этот метод извлекает данные о пользователе из свойства User.Identity в виде ApiUserIdentity и возвращает связанный объект UserDTO. Если пользователь не аутентифицирован или данные о пользователе недоступны, метод вернет значение null.
        /// </summary>
        /// <param name="contextAccessor"></param>
        /// <returns></returns>
        public static UserDto? GetApiUser(this IHttpContextAccessor contextAccessor)
        {
            var userData = (contextAccessor.HttpContext?.User?.Identity as ApiUserIdentity)?.UserData;
            return userData;
        }
        public static Guid? GetApiUserId(this IHttpContextAccessor contextAccessor)
        {
            var userData = (contextAccessor.HttpContext?.User?.Identity as ApiUserIdentity)?.UserData.Id;
            return userData;
        }
    }
}
