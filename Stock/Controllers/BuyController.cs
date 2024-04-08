using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stock.Application.Items.Queries;
using Stock.Application.StoreItems.Commands;
using Stock.Application.StoreItems.Queries;
using Stock.Application.Stores.Queries;

namespace Stock.Controllers;
public class BuyController(IMediator mediator) : Controller
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken token)
    {
        try
        {
            var stores = await _mediator.Send(new GetAllStoresQuery(), token);
            ViewBag.Stores = new SelectList(stores, "Id", "Name");

            var items = await _mediator.Send(new GetAllItemsQuery(), token);
            ViewBag.Items = new SelectList(items, "Id", "Name");

            return View();
        }
        catch
        {
            return RedirectToAction("Error", "Home");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetStoreItemQuantity(Guid storeId, Guid itemId, CancellationToken token)
    {
        try
        {
            var response = await _mediator.Send(new GetStoreItemQuantityQuery { StoreId = storeId, ItemId = itemId, }, token);
            return response is null ? throw new Exception() : Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> BuyItems([FromBody] BuyCommand command, CancellationToken token)
    {
        try
        {
            var response = await _mediator.Send(command, token);
            return response is null ? throw new Exception() : Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
