using System.Security.Cryptography;
using System.Text;

namespace GMD.PrivateMessenger.PL.API.Helpers;
/// <summary>
/// Статический класс Additional содержит дополнительные методы для различных операций, таких как очистка номера телефона, генерация случайных строк, генерация случайных целых чисел и хеширование паролей.
/// </summary>
public static class Additional
{
    private static readonly Random _rand = new Random();
    /// <summary>
    /// Очищает номер телефона, удаляя все символы, кроме цифр. Затем проверяет и преобразует номер телефона в определенный формат. Если номер телефона равен null, метод возвращает null.
    /// </summary>
    /// <param name="phone"></param>
    /// <returns></returns>
    public static string CleanPhone(string phone)
    {
        if (phone == null)
            return null;
        phone = new string(phone.Where(char.IsDigit).ToArray());
        if (phone.Length < 10 || phone.StartsWith("+"))
            return phone;
        if (phone.Length == 10)
            phone = "7" + phone;
        else if (phone.StartsWith("8"))
            phone = "7" + phone.Substring(1);
        return '+' + phone;
    }
    /// <summary>
    /// Генерирует случайную строку заданной длины с использованием указанного алфавита. По умолчанию, длина строки равна 32 символам, алфавит состоит из символов латинского алфавита (строчные и заглавные буквы) и цифр.
    /// </summary>
    /// <param name="length"></param>
    /// <param name="alphabet"></param>
    /// <returns></returns>
    public static string GenerateRandomString(int length = 32, string alphabet = "abcdefghijklmnopqrstuvwxyz0123456789")
    {
        var builder = new StringBuilder();
        for (var i = 0; i < length; ++i)
            builder.Append(alphabet[_rand.Next(0, alphabet.Length - 1)]);
        return builder.ToString();
    }
    /// <summary>
    /// Генерирует случайную строку, длина которой находится в заданном диапазоне от minLength до maxLength. Использует указанный алфавит для создания строки.
    /// </summary>
    /// <param name="minLength"></param>
    /// <param name="maxLength"></param>
    /// <param name="alphabet"></param>
    /// <returns></returns>
    public static string GenerateRandomString(int minLength, int maxLength, string alphabet = "abcdefghijklmnopqrstuvwxyz0123456789")
    {
        return GenerateRandomString(_rand.Next(minLength, maxLength), alphabet);
    }
    /// <summary>
    /// Генерирует случайное целое число в заданном диапазоне от minValue до maxValue.
    /// </summary>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    public static int GenerateRandomInt(int minValue, int maxValue)
    {
        return _rand.Next(minValue, maxValue + 1);
    }
    /// <summary>
    /// Вычисляет хеш пароля, используя алгоритм SHA-512. Принимает строку пароля, преобразует ее в массив байтов, вычисляет хеш и возвращает хеш в виде строки в шестнадцатеричном формате.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string GetPasswordHash(string s)
    {
        if (s == null)
            return null;
        using var hashAlgorithm = SHA512.Create();
        var hash = hashAlgorithm.ComputeHash(Encoding.Unicode.GetBytes(s));
        return string.Concat(hash.Select(item => item.ToString("x2")));
    }
}
