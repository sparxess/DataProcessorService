using DataProcessorService.Application.Contracts.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DataProcessorService.HttpApi.Host.Controllers;

[Authorize]
[ApiController]
[Route("api/data")]
public class DataController : ControllerBase
{
    private readonly IDataService _dataService;

    public DataController(IDataService dataService)
    {
        _dataService = dataService;
    }

    [HttpPost("items")]
    public async Task<IActionResult> SaveItems([FromBody] DataItemsRequestDto items)
    {
        await _dataService.SaveItemsAsync(items);

        return Ok();
    }

    [HttpGet("items")]
    public async Task<IActionResult> GetItems([FromQuery] GetDataItemsInput? input)
    {
        var data = await _dataService.GetItemsAsync(input);

        if(data == null)
        {
            return NotFound();
        }

        return Ok(data);
    }
}
