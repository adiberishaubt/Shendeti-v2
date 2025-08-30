using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shendeti.Domain.Interfaces;
using Shendeti.Extensions;
using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;
using Shendeti.Infrastructure.Utils;

namespace Shendeti.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        var tokenResponse = await _userService.LoginAsync(loginRequest);
        Response.Cookies.Append("jwt", tokenResponse.Token, new CookieOptions { Expires = DateTime.UtcNow.AddMinutes(1) });
        Response.Cookies.Append("refreshToken", tokenResponse.RefreshToken, new CookieOptions { Expires = DateTime.UtcNow.AddDays(7) });
        return Ok();
    }
    
    
    [HttpPost("doctor/register")]
    public async Task<IActionResult> RegisterDoctorAsync(RegisterDoctorRequest registerDoctorRequest)
    {
        await _userService.RegisterDoctorAsync(registerDoctorRequest);
        return Ok();
    }
    
    [HttpPost("patient/register")]
    public async Task<IActionResult> RegisterPatientAsync(RegisterPatientRequest registerPatientRequest)
    {
        await _userService.RegisterPatientAsync(registerPatientRequest);
        return Ok();
    }
    
    [Authorize]
    [HttpPut("update")]
    public async Task<IActionResult> UpdateUserAsync(UpdateUserRequest updateUserRequest)
    {
        await _userService.UpdateUserAsync(User.GetId(), updateUserRequest);
        return Ok();
    }


    [HttpGet("refreshToken")]
    public async Task<ActionResult> RefreshToken()
    {
        if (!Request.Cookies.TryGetValue("refreshToken", out var value))
        {
            // throw
        }
        
        var jwt = await _userService.RefreshToken(value);
        Response.Cookies.Append("jwt", jwt, new CookieOptions { Expires = DateTime.UtcNow.AddMinutes(30)});
        
        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteUser()
    {
        await _userService.DeleteUser(User.GetId());
        return Ok();
    }
    
    [HttpPost("filter")]
    public async Task<ActionResult<List<DoctorSearchDto>>> Filter(FilterDoctorDto filterDoctorDto)
    {
        return await _userService.FilterDoctors(filterDoctorDto);
    }
    
    [Authorize(Roles="Doctor")]
    [HttpGet("doctor")]
    public async Task<DoctorResponse> ReadDoctorAsync()
    {
        return await _userService.ReadDoctorAsync(User.GetId());
    }
    
    [Authorize]
    [HttpGet("me")]
    public async Task<UserResponse> ReadMyUserAsync()
    {
        return await _userService.ReadMyUserAsync(User.GetId(), User.GetRole());
    }

    [HttpGet("bloodTypes")]
    public IActionResult GetAllBloodTypes()
    {
        var bloodTypes = Enum.GetValues(typeof(BloodType));
        var bloodTypeList = new List<BloodTypeDto>();

        foreach (var bloodType in bloodTypes)
        {
            bloodTypeList.Add(new BloodTypeDto
            {
                Name = Enum.GetName(typeof(BloodType), bloodType),
                Id = (int)bloodType
            });
        }

        return Ok(bloodTypeList);
    }
    
}