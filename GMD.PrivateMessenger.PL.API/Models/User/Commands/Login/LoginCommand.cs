namespace GMD.PrivateMessenger.PL.API.Models.User.Commands.Login;

using GMD.PrivateMessenger.PL.API.Models;
using System.Text.RegularExpressions;

public class LoginUserCommand :
    IRequest<string>
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    private readonly Regex loginRegex = new Regex(@"^[a-zA-Z0-9_-]{3,16}$");
    private readonly Regex passwordRegex = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}$");
    public LoginUserCommandValidator()
    {
        RuleFor(u => u.Login).NotEmpty()
            .WithMessage(
            "Логин не должен быть пустым"
            );
        RuleFor(u => u.Password).NotEmpty()
            .WithMessage(
            "Пароль не должен быть пустым"
            );
        RuleFor(u => u.Login).Matches(loginRegex)
            .WithMessage(
            "Логин должен содержать только буквы (как заглавные, так и строчные), цифры, символы подчеркивания и дефисы. Длина логина должна быть от 3 до 16 символов."
            );
        RuleFor(u => u.Password).Matches(passwordRegex)
            .WithMessage(
            "Пароль должен содержать хотя-бы одно число, спецсимвол, латинскую букву в верхнем и нижнем регистре и состоять не менее чем из 6 символов."
            );
    }
}