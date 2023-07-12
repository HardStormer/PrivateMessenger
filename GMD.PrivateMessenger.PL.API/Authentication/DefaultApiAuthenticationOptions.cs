using Microsoft.AspNetCore.Authentication;

namespace GMD.PrivateMessenger.PL.API.Authentication
{
    /// <summary>
    /// Класс DefaultApiAuthenticationOptions является настройками схемы аутентификации по умолчанию в API.
    /// </summary>
    public class DefaultApiAuthenticationOptions : AuthenticationSchemeOptions
    {
        /// <summary>
        /// Константное поле, представляющее значение по умолчанию для имени схемы аутентификации. Значение "DefaultApiAuthentication" используется в качестве значения схемы по умолчанию при регистрации схемы аутентификации.
        /// </summary>
        public const string DefaultScheme = "DefaultApiAuthentication";
    }
}