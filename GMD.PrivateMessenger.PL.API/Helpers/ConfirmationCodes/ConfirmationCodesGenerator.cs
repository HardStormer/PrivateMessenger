using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace GMD.PrivateMessenger.PL.API.Helpers.ConfirmationCodes;
/// <summary>
/// Класс ConfirmationCodesGenerator представляет генератор и валидатор подтверждающих кодов (confirmation codes) на основе симметричного алгоритма шифрования.
/// </summary>
public class ConfirmationCodesGenerator
{
    private SymmetricAlgorithm algorithm;

    private byte[] key;

    private byte[] initVector;

    private long duration;

    private Func<IList<string>, bool> checkFunction;

    private char separator;

    private Encoding encoding;
    /// <summary>
    /// Конструктор класса, который принимает симметричный алгоритм шифрования, ключ инициализации, продолжительность действия кода, функцию проверки, кодировку и символ-разделитель. В конструкторе инициализируются поля класса и проверяются допустимость параметров.
    /// </summary>
    /// <param name="algorithm">Симметричный алгоритм шифрования, используемый для шифрования и дешифрования кодов.</param>
    /// <param name="key">Байтовый массив, представляющий ключ шифрования.</param>
    /// <param name="initVector">Байтовый массив, представляющий инициализационный вектор.</param>
    /// <param name="duration">Продолжительность (в секундах), в течение которой подтверждающий код считается действительным.</param>
    /// <param name="checkFunction">Функция проверки, которая принимает список строк и возвращает булево значение. Используется для дополнительной проверки извлеченных значений кода.</param>
    /// <param name="encoding">Символ-разделитель, используемый для разделения значений кода.</param>
    /// <param name="separator">Кодировка, используемая для преобразования текста в байты. По умолчанию, используется кодировка Unicode.</param>
    /// <exception cref="Exception"></exception>
    public ConfirmationCodesGenerator(SymmetricAlgorithm algorithm, string key, string initVector, long duration,
        Func<IList<string>, bool> checkFunction, Encoding encoding = null, char separator = '\0')
    {
        this.algorithm = algorithm;
        this.key = Encoding.Unicode.GetBytes(key);
        this.initVector = Encoding.Unicode.GetBytes(initVector);
        if (this.initVector.Length != algorithm.BlockSize / 8)
            throw new Exception($"Initialization vector should consist of {algorithm.BlockSize / 8} bytes");
        if (this.key.Length != algorithm.KeySize / 8)
            throw new Exception($"Key should consist of {algorithm.KeySize / 8} bytes");
        if (duration <= 0)
            throw new Exception("Duration should be positive");
        this.duration = duration;
        this.checkFunction = checkFunction;
        this.separator = separator;
        this.encoding = encoding ?? Encoding.Unicode;
    }
    /// <summary>
    /// Генерирует подтверждающий код на основе переданных значений. Метод создает шифратор на основе алгоритма и ключа, шифрует значения и текущую дату-время, а затем возвращает полученный код в виде строки шестнадцатеричных символов.
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public string Generate(params string[] values)
    {
        var encryptor = algorithm.CreateEncryptor(key, initVector);
        using (var msEncrypt = new MemoryStream())
        {
            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            {
                var arr = encoding.GetBytes(string.Join(new string(separator, 1), values) + separator +
                    DateTime.Now.Ticks);
                csEncrypt.Write(arr, 0, arr.Length);
            }
            return string.Join("", msEncrypt.ToArray().Select(item => item.ToString("x2")));
        }
    }
    /// <summary>
    /// Проверяет действительность подтверждающего кода. Метод принимает код в виде строки, дешифрует его и проверяет его действительность. Если код действителен, метод возвращает объект CodeValidationResult с извлеченными значениями и флагом действительности. Если код недействителен или возникает исключение при его обработке, метод возвращает объект CodeValidationResult без извлеченных значений.
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public CodeValidationResult Validate(string code)
    {
        if (string.IsNullOrEmpty(code) || code.Length % 2 == 1)
            return new CodeValidationResult();
        try
        {
            var array = new byte[code.Length / 2];
            for (var i = 0; i < code.Length; i += 2)
                array[i / 2] = byte.Parse(code.Substring(i, 2), NumberStyles.HexNumber);
            var decryptor = algorithm.CreateDecryptor(key, initVector);
            using (var msDecrypt = new MemoryStream(array))
            {
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (var srDecrypt = new StreamReader(csDecrypt, encoding))
                    {
                        var values = srDecrypt.ReadToEnd().Split(separator);
                        var time = new DateTime(long.Parse(values.Last()));
                        values = values.Take(values.Length - 1).ToArray();
                        if (time.AddSeconds(duration) < DateTime.Now)
                            return new CodeValidationResult();
                        var result = new CodeValidationResult
                        {
                            ExtractedValues = values,
                            IsValid = true
                        };
                        if (checkFunction != null)
                        {
                            result.IsValid = checkFunction(values.ToArray());
                            if (!result.IsValid)
                                result.ExtractedValues = null;
                        }
                        return result;
                    }
                }
            }
        }
        catch
        {
            return new CodeValidationResult();
        }
    }
}
