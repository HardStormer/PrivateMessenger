using GMD.PrivateMessenger.DAL.Interfaces;
using GMD.PrivateMessenger.PL.API.Authentication;
using GMD.PrivateMessenger.PL.API.Models.User.Commands.Delete;
using GMD.PrivateMessenger.PL.API.Models.User.Commands.Login;
using GMD.PrivateMessenger.PL.API.Models.User.Commands.Logout;
using GMD.PrivateMessenger.PL.API.Models.User.Commands.Register;
using GMD.PrivateMessenger.PL.API.Models.User.Commands.Update;
using GMD.PrivateMessenger.PL.API.Models.User.Queries;
using GMD.PrivateMessenger.PL.API.Models.User.Queries.Get;
using GMD.PrivateMessenger.PL.API.Models.User.Queries.GetMyProfile;

namespace GMD.PrivateMessenger.PL.API.Controllers;


public class UserController : BaseController
{
    protected readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// метод предназначен для получения отдельного элемента данных
    /// </summary>
    /// <param name="query">идентификатор типа Guid</param>
    /// <returns></returns>
    [HttpGet]
    public virtual async Task<ActionResult<UserViewModel>> Get([FromQuery] GetUserQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }
    
    /// <summary>
    /// метод предназначен для получения отдельного элемента данных
    /// </summary>
    /// <param name="query">идентификатор типа Guid</param>
    /// <returns></returns>
    [HttpGet]
    public virtual async Task<ActionResult<UserViewModel>> Get([FromQuery] GetMyProfileQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }
    /// <summary>
    /// Авторизация
    /// </summary>
    /// <param name="loginUserCommand"></param>
    /// <returns>Токен</returns>
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<LoginUserCommandResponce>> LogIn(LoginUserCommand loginUserCommand)
    {
        var result = await Mediator.Send(loginUserCommand);

        return Ok(result);
    }
    /// <summary>
    /// Регистрация
    /// </summary>
    /// <param name="registerUserCommand"></param>
    /// <returns>Токен</returns>
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<RegisterUserCommandResponce>> Register(RegisterUserCommand registerUserCommand)
    {
        var result = await Mediator.Send(registerUserCommand);

        return Ok(result);
    }
    /// <summary>
    /// Выход
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> LogOut()
    {
        var user = this.GetApiUser();
        if (user == null)
            return BadRequest("Bad token");

        var logoutUserCommand = new LogoutUserCommand
        {
            UserId = user.Id,
        };
        await Mediator.Send(logoutUserCommand);

        return Ok();
    }
    /// <summary>
    /// Редактирование пароля
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> ChangePassword(UpdateUserPasswordCommandRequest updateUserPasswordCommandRequest)
    {
        var user = this.GetApiUser();
        if (user == null)
            return BadRequest("Bad token");

        var updateUserPasswordCommand = new UpdateUserPasswordCommand
        {
            UserId = user.Id,
            OldPassword = updateUserPasswordCommandRequest.OldPassword,
            Password = updateUserPasswordCommandRequest.Password
        };

        await Mediator.Send(updateUserPasswordCommand);

        return Ok();
    }
    /// <summary>
    /// Редактирование имени
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> ChangeName(UpdateUserNameCommandRequest updateUserNameCommandRequest)
    {
        var user = this.GetApiUser();
        if (user == null)
            return BadRequest("Bad token");

        var updateUserNameCommand = new UpdateUserNameCommand
        {
            UserId = user.Id,
            Name = updateUserNameCommandRequest.Name
        };

        await Mediator.Send(updateUserNameCommand);

        return Ok();
    }
    /// <summary>
    /// Удалить мой аккаунт
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> DeleteMyAccount()
    {
        var user = this.GetApiUser();
        if (user == null)
            return BadRequest("Bad token");

        var deleteUserCommand = new DeleteUserCommand
        {
            UserId = user.Id
        };

        await Mediator.Send(deleteUserCommand);

        return Ok();
    }
}
