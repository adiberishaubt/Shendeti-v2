using Microsoft.AspNetCore.Mvc;
using Shendeti.Domain.Interfaces;
using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;

namespace Shendeti.Controllers;

[ApiController]
[Route("[controller]")]
public class CityController : ControllerBase
{
    private readonly ICityService _cityService;

    public CityController(ICityService cityService)
    {
        _cityService = cityService;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<City>>> ReadAllAsync(int pageIndex = 1, int pageSize = 10)
    {
        return await _cityService.ReadAllAsync(pageIndex, pageSize);
    }
    
    [HttpGet("list")]
    public async Task<ActionResult<List<City>>> ReadAllAsync()
    {
        return await _cityService.ReadAllAsync();
    }

    [HttpGet("id")]
    public async Task<ActionResult<City>> ReadAsync(int id)
    {
        return await _cityService.ReadAsync(id);
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(CityRequest cityRequest)
    {
        await _cityService.CreateAsync(cityRequest);
        return Ok();
    }

    [HttpPut("id")]
    public async Task<ActionResult> UpdateAsync(int id, CityRequest cityRequest)
    {
        await _cityService.UpdateAsync(id, cityRequest);
        return Ok();
    }

    [HttpDelete("id")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        await _cityService.DeleteAsync(id);
        return Ok();
    }
}