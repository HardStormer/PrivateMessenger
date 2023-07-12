using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using GMD.PrivateMessenger.DAL.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace GMD.PrivateMessenger.PL.API.Authentication
{
    /// <summary>
    /// Класс DefaultApiAuthenticationHandler является реализацией обработчика аутентификации для аутентификации по умолчанию в API.
    /// </summary>
    public class DefaultApiAuthenticationHandler : AuthenticationHandler<DefaultApiAuthenticationOptions>
    {
        private readonly JsonSerializerOptions serializerSettings;
        private readonly IUserRepository _userRepo;
        /// <summary>
        /// Конструктор класса, который принимает различные зависимости, необходимые для обработки аутентификации. Включает IUserRepository для доступа к хранилищу пользователей, IOptionsMonitor для получения параметров аутентификации, ILoggerFactory для логирования, UrlEncoder для кодирования URL, ISystemClock для доступа к текущему времени, и IOptions для получения настроек сериализатора JSON.
        /// </summary>
        /// <param name="userRepo"></param>
        /// <param name="options"></param>
        /// <param name="logger"></param>
        /// <param name="encoder"></param>
        /// <param name="clock"></param>
        /// <param name="serializerOptions"></param>
        public DefaultApiAuthenticationHandler(
            IUserRepository userRepo,
            IOptionsMonitor<DefaultApiAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IOptions<Microsoft.AspNetCore.Mvc.JsonOptions> serializerOptions) : base(options, logger, encoder, clock)
        {
            _userRepo = userRepo;
            serializerSettings = serializerOptions.Value.JsonSerializerOptions;
        }
        /// <summary>
        /// Переопределенный метод, который выполняет аутентификацию пользователя на основе предоставленного токена аутентификации. Метод извлекает токен из заголовка "Authorization" запроса, проверяет его корректность и связывает его с соответствующим пользователем в хранилище пользователей. Если аутентификация проходит успешно, метод возвращает результат аутентификации с объектом ClaimsPrincipal, представляющим пользователя, и схемой аутентификации по умолчанию.
        /// </summary>
        /// <returns></returns>
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            string? token = null;
            if (Request.Headers.ContainsKey("Authorization"))
            {
                token = Request.Headers["Authorization"].ToString();
                if (token.StartsWith("Bearer "))
                {
                    token = token.Substring(7);
                }
            }
            if (token == null)
            {
                return AuthenticateResult.NoResult();
            }
            var login = TokenProcessor.GetUserLogin(token)!;

            var user = await _userRepo.GetAsync(login!);
            if (user == null)
            {
                return AuthenticateResult.Fail("Incorrect token");
            }

            if (user.TokenExpiredAt < DateTime.UtcNow)
            {
                return AuthenticateResult.Fail("Token expired");
            }
            return AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(new ApiUserIdentity(user)),
                DefaultApiAuthenticationOptions.DefaultScheme));
        }
        /// <summary>
        /// Переопределенный метод, который обрабатывает ситуацию вызова вызова аутентификации, когда требуется аутентификация пользователя. В данном случае, метод вызывает метод HandleForbiddenAsync, чтобы вернуть ошибку 401 Unauthorized.
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            return HandleForbiddenAsync(properties);
        }
        /// <summary>
        /// Переопределенный метод, который обрабатывает ситуацию, когда доступ к ресурсу запрещен из-за отсутствия аутентификации или недостаточных прав. Метод устанавливает статус кода ответа 401 Unauthorized и возвращает пустой JSON-ответ.
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        protected override async Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            Response.StatusCode = 401;
            await Response.WriteAsync(JsonConvert.SerializeObject(null));
        }
    }
}