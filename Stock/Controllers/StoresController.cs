using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stock.Application.Stores.Commands;
using Stock.Application.Stores.Queries;

namespace Stock.Controllers;
public class StoresController(IMediator mediator) : Controller
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
    public async Task<IActionResult> GetAllStores(CancellationToken token)
    {
        try
        {
            var stores = await _mediator.Send(new GetAllStoresQuery(), token);
            return Ok(stores);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddStoreCommand command, CancellationToken token)
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
    public async Task<IActionResult> Edit([FromBody] EditStoreCommand command, CancellationToken token)
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
            var response = await _mediator.Send(new DeleteStoreCommand
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
