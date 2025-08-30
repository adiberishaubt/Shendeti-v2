using Microsoft.AspNetCore.Mvc;
using Shendeti.Domain.Interfaces;
using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;
using Shendeti.Infrastructure.Interfaces;

namespace Shendeti.Controllers;

[ApiController]
[Route("[controller]")]
public class ServiceController : ControllerBase
{
    private readonly IServiceService _serviceService;

    public ServiceController(IServiceService serviceService)
    {
        _serviceService = serviceService;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<Service>>> ReadAllAsync(int pageIndex = 1, int pageSize = 10)
    {
        return await _serviceService.ReadAllAsync(pageIndex, pageSize);
    }
    
    [HttpGet("id")]
    public async Task<ActionResult<Service>> ReadAsync(int id)
    {
        return await _serviceService.ReadAsync(id);
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(ServiceRequest serviceRequest)
    {
        await _serviceService.CreateAsync(serviceRequest);
        return Ok();
    }

    [HttpPut("id")]
    public async Task<ActionResult> UpdateAsync(int id, ServiceRequest serviceRequest)
    {
        await _serviceService.UpdateAsync(id, serviceRequest);
        return Ok();
    }

    [HttpDelete("id")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        await _serviceService.DeleteAsync(id);
        return Ok();
    }
}