using System.Text.RegularExpressions;

namespace GMD.PrivateMessenger.PL.API.Models.User.Commands.Update;
public class UpdateUserPasswordCommandRequest
{
    public string OldPassword { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
public class UpdateUserNameCommandRequest
{
    public string? Name { get; set; }
}
public class UpdateUserPasswordCommand : CRUDCommand,
    IRequest
{
    public Guid UserId { get; set; }
    public string OldPassword { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
public class UpdateUserNameCommand : CRUDCommand,
    IRequest
{
    public Guid UserId { get; set; }
    public string? Name { get; set; }
}
public class UpdateUserPasswordCommandValidator : AbstractValidator<UpdateUserPasswordCommand>
{
    private readonly Regex passwordRegex = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}$");
    public UpdateUserPasswordCommandValidator()
    {
        RuleFor(u => u.Password).NotEmpty()
            .WithMessage(
            "Пароль не должен быть пустым"
            );
        RuleFor(u => u.Password).Matches(passwordRegex)
            .WithMessage(
            "Пароль должен содержать хотя-бы одно число, спецсимвол, латинскую букву в верхнем и нижнем регистре и состоять не менее чем из 6 символов."
            );
    }
}
public class UpdateUserNameCommandValidator : AbstractValidator<UpdateUserNameCommand>
{
    private readonly Regex nameRegex = new Regex(@"^[A-Za-zА-Яа-яЁё\s'-]{1,50}$");
    public UpdateUserNameCommandValidator()
    {
        RuleFor(u => u.Name).Matches(nameRegex)
            .WithMessage(
            "Имя должно содержать только буквы (как заглавные, так и строчные) на латинице и кириллице, пробелы, апострофы и дефисы в имени. Длина имени должна быть от 1 до 50 символов."
            );
    }
}
