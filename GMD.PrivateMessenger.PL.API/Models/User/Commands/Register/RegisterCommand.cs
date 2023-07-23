using System.Text.RegularExpressions;

namespace GMD.PrivateMessenger.PL.API.Models.User.Commands.Register;


public class RegisterUserCommand :
    IRequest<RegisterUserCommandResponce>
{
    public string? Name { get; set; }
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    private readonly Regex nameRegex = new Regex(@"^[A-Za-zА-Яа-яЁё\s'-]{1,50}$");
    private readonly Regex loginRegex = new Regex(@"^[a-zA-Z0-9_-]{3,16}$");
    private readonly Regex passwordRegex = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}$");
    public RegisterUserCommandValidator()
    {
        RuleFor(u => u.Login).NotEmpty()
            .WithMessage(
            "Логин не должен быть пустым"
            );
        RuleFor(u => u.Password).NotEmpty()
            .WithMessage(
            "Пароль не должен быть пустым"
            );
        RuleFor(u => u.Name).Matches(nameRegex)
            .WithMessage(
            "Имя должно содержать только буквы (как заглавные, так и строчные) на латинице и кириллице, пробелы, апострофы и дефисы в имени. Длина имени должна быть от 1 до 50 символов."
            );
        RuleFor(u => u.Login).Matches(loginRegex)
            .WithMessage(
            "Логин должен содержать только буквы (как заглавные, так и строчные), цифры, символы подчеркивания и дефисы. Длина логина должна быть от 3 до 16 символов."
            );
        RuleFor(u => u.Password).Matches(passwordRegex)
            .WithMessage(
            "Пароль должен содержать хотя-бы одно число, латинскую букву в верхнем и нижнем регистре и состоять не менее чем из 6 символов."
            );
    }
}
