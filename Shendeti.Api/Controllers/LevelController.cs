using Microsoft.AspNetCore.Mvc;
using Shendeti.Domain.Interfaces;
using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;
using Shendeti.Infrastructure.Interfaces;

namespace Shendeti.Controllers;

[ApiController]
[Route("[controller]")]
public class LevelController : ControllerBase
{
    private readonly ILevelService _levelService;

    public LevelController(ILevelService levelService)
    {
        _levelService = levelService;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<Level>>> ReadAllAsync(int pageIndex = 1, int pageSize = 10)
    {
        return await _levelService.ReadAllAsync(pageIndex, pageSize);
    }

    [HttpGet("id")]
    public async Task<ActionResult<Level>> ReadAsync(int id)
    {
        return await _levelService.ReadAsync(id);
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(LevelRequest levelRequest)
    {
        await _levelService.CreateAsync(levelRequest);
        return Ok();
    }

    [HttpPut("id")]
    public async Task<ActionResult> UpdateAsync(int id, LevelRequest levelRequest)
    {
        await _levelService.UpdateAsync(id, levelRequest);
        return Ok();
    }

    [HttpDelete("id")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        await _levelService.DeleteAsync(id);
        return Ok();
    }
}