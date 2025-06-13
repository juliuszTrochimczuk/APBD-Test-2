using API.DTOs;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("/api/records")]
public class RecordController : ControllerBase
{
    public readonly IRecordService service;

    public RecordController(IRecordService service)
    {
        this.service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRecords([FromQuery] string filter)
    {
        try
        {
            if (string.IsNullOrEmpty(filter))
                return Ok(service.GetAllRecordsAsync());
            return Ok(service.GetAllRecordsAsync(filter));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddRecord([FromBody] CreateRecordRequestBody body)
    {
        try
        {
            var result = await service.CreateNewRecordAsync(body);
            return Created("/created-record/" + result.Id, result);
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}