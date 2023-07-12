namespace GMD.PrivateMessenger.PL.API.Helpers.ConfirmationCodes;
/// <summary>
/// Класс CodeValidationResult представляет результат проверки действительности подтверждающего кода.
/// </summary>
public class CodeValidationResult
{
    /// <summary>
    /// Логическое значение, указывающее, является ли код действительным (true) или нет (false).
    /// </summary>
    public bool IsValid { get; set; }
    /// <summary>
    /// Список строк, содержащих извлеченные значения из подтверждающего кода. Может быть null, если значения не были извлечены или код недействителен.
    /// </summary>
    public IList<string> ExtractedValues { get; set; }
}
