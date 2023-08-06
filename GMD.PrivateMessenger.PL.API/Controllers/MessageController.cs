using GMD.PrivateMessenger.PL.API.Models.Message.Commands.Create;
using GMD.PrivateMessenger.PL.API.Models.Message.Commands.Delete;
using GMD.PrivateMessenger.PL.API.Models.Message.Commands.Update;
using GMD.PrivateMessenger.PL.API.Models.Message.Queries;
using GMD.PrivateMessenger.PL.API.Models.Message.Queries.Get;
using GMD.PrivateMessenger.PL.API.Models.Message.Queries.GetList;

namespace GMD.PrivateMessenger.PL.API.Controllers;

public class MessageController : BaseCrudController<
    CreateMessageCommand,
    DeleteMessageCommand,
    UpdateMessageCommand,
    MessageViewModel,
    MessageListViewModel,
    GetMessageQuery,
    GetMessageListQuery>
{
    /// <summary>
    /// метод предназначен для получения пагинированного списка элементов
    /// </summary>
    /// <param name="query">содержит информацию о параметрах пагинации и фильтрации</param>
    /// <returns></returns>
    [HttpGet]
    public virtual async Task<ActionResult<MessageListViewModel>> GetAllPagedByText([FromQuery] GetMessageListByTextQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }
}
