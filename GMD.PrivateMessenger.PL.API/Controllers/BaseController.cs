namespace GMD.PrivateMessenger.PL.API.Controllers;
/// <summary>
/// Абстрактный класс BaseController является базовым классом для контроллеров.
/// </summary>
[Authorize]
[ApiController]
[Route("[controller]/[action]")]
public abstract class BaseController : ControllerBase
{
    private IMediator? _mediator;
    private IMapper? _mapper;
    /// <summary>
    /// Свойство, представляющее экземпляр IMediator. Если _mediator не был инициализирован, свойство получает его через доступ к службам (services) HttpContext.RequestServices. Если служба не найдена, возникнет исключение.
    /// </summary>
    protected IMediator Mediator =>
        _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
    /// <summary>
    /// Свойство, представляющее экземпляр IMapper. Если _mapper не был инициализирован, свойство получает его через доступ к службам HttpContext.RequestServices. Если служба не найдена, возникнет исключение.
    /// </summary>
    protected IMapper Mapper =>
        _mapper ??= HttpContext.RequestServices.GetRequiredService<IMapper>();
}
