namespace GMD.PrivateMessenger.PL.API.Common.Exceptions;
/// <summary>
/// Класс NotFoundException представляет исключение, которое возникает при попытке найти сущность, которая не была найдена.
/// </summary>
public class NotFoundException : Exception
{
    /// <summary>
    /// Конструктор класса, который принимает имя сущности и ключ в качестве параметров. Он вызывает базовый конструктор класса Exception, передавая сообщение об ошибке, которое состоит из имени сущности и ключа, и указывает, что сущность не была найдена.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="key"></param>
    public NotFoundException(string name, object key)
        : base($"Entity \"{name}\" ({key}) not found.") { }
}
