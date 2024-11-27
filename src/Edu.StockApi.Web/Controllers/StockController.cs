using Edu.StockApi.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Edu.StockApi.HttpModels;

namespace Edu.StockApi.Web.Controllers;

[ApiController]
[Route("v1/api/stocks")]
public class StockController: ControllerBase
{
    private readonly IStockService _stockService;

    public StockController(IStockService stockService)
    {
        _stockService = stockService;
    }

    [HttpGet]
    public async Task<ActionResult<List<StockItem>>> GetAll(CancellationToken token)
    {
        var stockItems = await _stockService.GetAll(token);
        return Ok(stockItems);
    }

    [HttpGet("id:long")]
    public async Task<ActionResult<StockItem>> GetById(long id, CancellationToken token)
    {
        var stockItem = await _stockService.GetByID(id, token);
        if (stockItem is null)
        {
            return NotFound();
        }
        return Ok(stockItem);
    }
    
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