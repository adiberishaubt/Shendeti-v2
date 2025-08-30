using Microsoft.AspNetCore.Mvc;
using Shendeti.Domain.Interfaces;
using Shendeti.Extensions;
using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;

namespace Shendeti.Controllers;

[ApiController]
[Route("[controller]")]
public class BloodRequestController : ControllerBase
{
    private readonly IBloodRequestService _bloodRequestService;

    public BloodRequestController(IBloodRequestService bloodRequestService)
    {
        _bloodRequestService = bloodRequestService;
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(BloodRequestDto bloodRequest)
    {
        await _bloodRequestService.CreateAsync(bloodRequest, User.GetId());
        return Ok();
    }
    
    [HttpGet]
    public async Task<ActionResult<List<BloodRequest>>> ReadAllAsync()
    {
        return await _bloodRequestService.ReadAllAsync();
    }
    
    [HttpGet("id")]
    public async Task<ActionResult<BloodRequest>> ReadAsync(int id)
    {
        return await _bloodRequestService.ReadAsync(id);
    }
    
    [HttpPost("id")]
    public async Task<ActionResult> AcceptRequestAsync(int id)
    {
        await _bloodRequestService.AcceptRequestAsync(id, User.GetId());
        return Ok();
    }

    [HttpPost("requestId/donorId")]
    public async Task<ActionResult> ConfirmRequestCompletionAsync(int requestId, string donorId)
    {
        await _bloodRequestService.ConfirmRequestCompletionAsync(requestId, donorId);
        return Ok();
    }

    
    [HttpDelete("id")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        await _bloodRequestService.DeleteAsync(id, User.GetId());
        return Ok();
    }
}