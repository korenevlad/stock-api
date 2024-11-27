using Edu.StockApi.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Edu.StockApi.HttpModels;

namespace Edu.StockApi.Web.Controllers;

[ApiController]
[Route("v1/api/stocks")]
[Produces("application/json")]
public class StockController: ControllerBase
{
    private readonly IStockService _stockService;

    public StockController(IStockService stockService)
    {
        _stockService = stockService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<StockItem>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken token)
    {
        var stockItems = await _stockService.GetAll(token);
        return Ok(stockItems);
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(StockItem), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(long id, CancellationToken token)
    {
        var stockItem = await _stockService.GetByID(id, token);
        if (stockItem is null)
        {
            return NotFound();
        }
        return Ok(stockItem);
    }
    
    /// <summary>
    /// Добавляет stock item.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<StockItem>> Add(StockItemPostViewModel item, CancellationToken token)
    {
        var createdStockItem = await _stockService.Add(new StockItemCreationModel
        {
            ItemName = item.ItemName,
            ItemQuantity = item.ItemQuantity
        }, token);
        return Ok(createdStockItem);
    }

    // public async Task<ActionResult<StockItem>> Update(long id, StockItemPutViewModel model, CancellationToken token)
    // {
    //     
    // } 
}