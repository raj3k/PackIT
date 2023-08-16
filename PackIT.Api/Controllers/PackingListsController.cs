using MediatR;
using Microsoft.AspNetCore.Mvc;
using PackIT.Application.Commands;
using PackIT.Application.Commands.Handlers;
using PackIT.Application.Dto;
using PackIT.Application.Queries;
using PackIT.Shared.Abstractions.Commands;
using PackIT.Shared.Abstractions.Queries;

namespace PackIT.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PackingListsController : BaseController
{
    // private readonly IQueryDispatcher _queryDispatcher;
    // private readonly ICommandDispatcher _commandDispatcher;
    private readonly IMediator _mediator;

    public PackingListsController(IMediator mediator)
    {
        // _queryDispatcher = queryDispatcher;
        // _commandDispatcher = commandDispatcher;
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PackingListDto>> Get([FromRoute] GetPackingList query, Guid id)
    {

        var result = await _mediator.Send(query);

        return OkOrNotFound(result);

        // var result = await _queryDispatcher.DispatchQueryAsync(query);
        //
        // return OkOrNotFound(result);
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PackingListDto>>> Get([FromQuery] SearchPackingLists query)
    {
        var result = await _mediator.Send(query);
    
        return OkOrNotFound(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreatePackingListWithItems command)
    {
        // await _commandDispatcher.DispatchAsync(command);
        await _mediator.Send(command);

        return CreatedAtAction(nameof(Get), new { id = command.Id }, null);
    }
    
    [HttpPut("{packingListId}/items")]
    public async Task<IActionResult> Put([FromBody] AddPackingItem command)
    {
        // await _commandDispatcher.DispatchAsync(command);
        await _mediator.Send(command);
        
        return Ok();
    }
    
    [HttpPut("{packingListId}/items/{name}/pack")]
    public async Task<IActionResult> Put([FromBody] PackItem command)
    {
        // await _commandDispatcher.DispatchAsync(command);
        await _mediator.Send(command);

        return Ok();
    }

    [HttpDelete("{packingListId}/items/{name}")]
    public async Task<IActionResult> Delete([FromBody] RemovePackingItem command)
    {
        // await _commandDispatcher.DispatchAsync(command);
        await _mediator.Send(command);
        
        return Ok();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromBody] DeletePackingList command)
    {
        // await _commandDispatcher.DispatchAsync(command);
        await _mediator.Send(command);
        
        return Ok();
    }
}