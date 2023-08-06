namespace GMD.PrivateMessenger.PL.API.Controllers;

public class BaseCrudController<TCreateCommand, TDeleteCommand, TUpdateCommand, TViewModel, TListViewModel, TQuery, TQueryList> : BaseController
where TCreateCommand : BaseCreateCommand<TViewModel>
where TDeleteCommand : BaseDeleteCommand
where TUpdateCommand : BaseUpdateCommand
where TViewModel : BaseViewModel
where TListViewModel : BaseListViewModel<TViewModel>
where TQuery : BaseGetQuery<TViewModel>
where TQueryList : BaseGetListQuery<TListViewModel, TViewModel>
{
    /// <summary>
    /// метод предназначен для получения пагинированного списка элементов
    /// </summary>
    /// <param name="query">содержит информацию о параметрах пагинации</param>
    /// <returns></returns>
    [HttpGet]
    public virtual async Task<ActionResult<TListViewModel>> GetAllPaged([FromQuery]TQueryList query)
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
    public virtual async Task<ActionResult<TViewModel>> Get([FromQuery]TQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// метод предназначен для создания нового элемента данных
    /// </summary>
    /// <param name="command">экземпляр</param>
    /// <returns></returns>
    [HttpPost]
    public virtual async Task<ActionResult<TViewModel>> Create(TCreateCommand command)
    {
        var result = await Mediator.Send(command);

        return CreatedAtAction(nameof(Get),  $"Id={result.Id.ToString()}", result);
    }

    /// <summary>
    /// метод предназначен для редактирования элемента данных
    /// </summary>
    /// <param name="command">экземпляр</param>
    /// <returns></returns>
    [HttpPut]
    public virtual async Task<IActionResult> Edit(TUpdateCommand command)
    {
        await Mediator.Send(command);
        return Ok(command);
    }

    /// <summary>
    /// метод предназначен удаления отдельного элемента данных
    /// </summary>
    /// <param name="command">идентификатор типа Guid и параметр определяющий тип удаления (по умолчанию true)</param>
    /// <returns></returns>
    [HttpDelete]
    public virtual async Task<IActionResult> Delete(TDeleteCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
}
