using GMD.PrivateMessenger.PL.API.Helpers;
using GMD.PrivateMessenger.PL.API.Helpers.ConfirmationCodes;
using System.Security.Cryptography;

namespace GMD.PrivateMessenger.PL.API.Authentication
{
    /// <summary>
    /// предоставляет функциональность для обработки токенов и генерации кодов подтверждения
    /// </summary>
    public static class TokenProcessor
    {
        private static readonly ConfirmationCodesGenerator generator;
        static TokenProcessor()
        {
            generator = new ConfirmationCodesGenerator(new AesCryptoServiceProvider(), "Frh!zp0IqSz2KxkV", "KOcg!Eo*",
                60L * 60 * 24 * 365, null);
        }
        /// <summary>
        /// Возвращает токен, сгенерированный для указанного объекта UserDTO. Токен содержит информацию о пользователе, такую как идентификатор, логин и другие параметры. Токен генерируется с использованием случайно сгенерированных строк и текущего времени.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string GetToken(this UserDto user)
        {
            return generator.Generate(Additional.GenerateRandomString(32, 64), user.Id.ToString()!,
                DateTime.Now.Ticks.ToString(), Additional.GenerateRandomString(32, 64), user.Login!.ToString());
        }
        /// <summary>
        /// Возвращает токен обновления, сгенерированный для указанного объекта UserDTO. Токен обновления используется для обновления и продления срока действия основного токена. Токен обновления генерируется с использованием случайно сгенерированных строк и текущего времени.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string GetRefreshToken(this UserDto user)
        {
            return generator.Generate(Additional.GenerateRandomString(16, 32), user.Id.ToString()!,
                DateTime.Now.Ticks.ToString(), Additional.GenerateRandomString(16, 32), user.Login!.ToString());
        }
        /// <summary>
        /// Асинхронно извлекает номер телефона пользователя из указанного токена. Если токен не валиден или не содержит необходимых данных, метод возвращает null. Если токен содержит номер телефона пользователя, метод возвращает этот номер. В случае возникновения ошибки при обработке токена, метод также возвращает null.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string? GetUserLogin(string token)
        {
            try
            {
                if (string.IsNullOrEmpty(token))
                    return null;
                var result = generator.Validate(token);
                if (!result.IsValid || result.ExtractedValues == null || result.ExtractedValues.Count < 5)
                    return null;
                else if (result.ExtractedValues[4] != null)
                    return result.ExtractedValues[4];
                else return null;
            }
            catch
            {
                return null;
            }
        }
    }
}