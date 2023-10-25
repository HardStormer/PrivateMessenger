using GMD.PrivateMessenger.PL.API.Models.Room.Commands.Create;
using GMD.PrivateMessenger.PL.API.Models.Room.Commands.Delete;
using GMD.PrivateMessenger.PL.API.Models.Room.Commands.Update;
using GMD.PrivateMessenger.PL.API.Models.Room.Queries;
using GMD.PrivateMessenger.PL.API.Models.Room.Queries.Get;
using GMD.PrivateMessenger.PL.API.Models.Room.Queries.GetList;

namespace GMD.PrivateMessenger.PL.API.Controllers;

public class RoomController : BaseCrudController<
    CreateRoomCommand,
    DeleteRoomCommand,
    UpdateRoomCommand,
    RoomViewModel,
    RoomListViewModel,
    GetRoomQuery,
    GetRoomListQuery>
{
    /// <summary>
    /// метод предназначен для получения пагинированного списка элементов
    /// </summary>
    /// <param name="query">содержит информацию о параметрах пагинации и фильтрации</param>
    /// <returns></returns>
    [HttpGet]
    public virtual async Task<ActionResult<RoomListViewModel>> GetAllPagedByName([FromQuery] GetRoomListByNameQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }
    /// <summary>
    /// метод предназначен для получения пагинированного списка элементов
    /// </summary>
    /// <param name="query">содержит информацию о параметрах пагинации и фильтрации</param>
    /// <returns></returns>
    [HttpGet]
    public virtual async Task<ActionResult<RoomListViewModel>> GetAllPagedByUserId([FromQuery] GetRoomListByUserIdQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }
}
