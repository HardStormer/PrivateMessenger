namespace GMD.PrivateMessenger.PL.API.Common.Exceptions;
/// <summary>
/// Класс ExpectedException представляет исключение, которое может быть ожидаемым и имеет дополнительное поле для представления статусного кода HTTP.
/// </summary>
public class ExpectedException : Exception
{
    /// <summary>
    /// Поле, представляющее статусный код HTTP, связанный с исключением.
    /// </summary>
    public HttpStatusCode httpStatusCode;
    /// <summary>
    /// Конструктор класса, который принимает сообщение об ошибке и статусный код HTTP. Он вызывает базовый конструктор класса Exception, передавая сообщение об ошибке, а затем устанавливает поле httpStatusCode, чтобы хранить статусный код HTTP.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="httpStatusCode"></param>
    public ExpectedException(string message, HttpStatusCode httpStatusCode)
        : base(message)
    {
        this.httpStatusCode = httpStatusCode;
    }
}
