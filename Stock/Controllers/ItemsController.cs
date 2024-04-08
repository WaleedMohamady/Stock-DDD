using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stock.Application.Items.Commands;
using Stock.Application.Items.Queries;

namespace Stock.Controllers;
public class ItemsController(IMediator mediator) : Controller
{
    private readonly IMediator _mediator = mediator;
    [HttpGet]
    public IActionResult Index()
    {
        try
        {
            return View();
        }
        catch
        {
            return RedirectToAction("Error", "Home");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllItems(CancellationToken token)
    {
        try
        {
            var items = await _mediator.Send(new GetAllItemsQuery(), token);
            return Ok(items);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddItemCommand command, CancellationToken token)
    {
        try
        {
            var response = await _mediator.Send(command, token);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Edit([FromBody] EditItemCommand command, CancellationToken token)
    {
        try
        {
            var response = await _mediator.Send(command, token);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id, CancellationToken token)
    {
        try
        {
            var response = await _mediator.Send(new DeleteItemCommand
            {
                Id = id
            }, token);
            return Ok(new { response });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
