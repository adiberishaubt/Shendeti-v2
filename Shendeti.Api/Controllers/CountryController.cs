using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shendeti.Domain.Interfaces;
using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;

namespace Shendeti.Controllers;

[ApiController]
[Route("[controller]")]
public class CountryController : ControllerBase
{
    private readonly ICountryService _countryService;

    public CountryController(ICountryService countryService)
    {
        _countryService = countryService;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<Country>>> ReadAllAsync(int pageIndex = 1, int pageSize = 10)
    {
        return await _countryService.ReadAllAsync(pageIndex, pageSize);
    }
    
    [HttpGet("list")]
    public async Task<ActionResult<List<Country>>> ReadAllAsync()
    {
        return await _countryService.ReadAllAsync();
    }

    [HttpGet("id")]
    public async Task<ActionResult<Country>> ReadAsync(int id)
    {
        return await _countryService.ReadAsync(id);
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(CountryRequest countryRequest)
    {
        await _countryService.CreateAsync(countryRequest);
        return Ok();
    }

    [HttpPut("id")]
    public async Task<ActionResult> UpdateAsync(int id, CountryRequest countryRequest)
    {
        await _countryService.UpdateAsync(id, countryRequest);
        return Ok();
    }

    [HttpDelete("id")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        await _countryService.DeleteAsync(id);
        return Ok();
    }

    [HttpGet("id/cities")]
    public async Task<ActionResult<List<City>>> GetCitiesAsync(int id)
    {
        return await _countryService.ReadCitiesAsync(id);
    }
}