namespace GMD.PrivateMessenger.PL.API.Models.User.Commands.Login;

using GMD.PrivateMessenger.PL.API.Models;
using System.Text.RegularExpressions;

public class LoginUserCommandResponce
{
    public string Token { get; set; } = string.Empty;
}