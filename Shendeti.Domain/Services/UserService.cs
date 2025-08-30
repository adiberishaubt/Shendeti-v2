using System.Transactions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shendeti.Domain.Interfaces;
using Shendeti.Infrastructure.Data;
using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;
using Shendeti.Infrastructure.Exceptions;
using Shendeti.Infrastructure.Extensions;
using Shendeti.Infrastructure.Utils;

namespace Shendeti.Domain.Services;

public class UserService : IUserService
{
    private readonly IJwtService _jwtService;
    private readonly UserManager<User> _userManager;
    private readonly DataContext _dataContext;

    public UserService(UserManager<User> userManager, IJwtService jwtService, DataContext dataContext)
    {
        _userManager = userManager;
        _jwtService = jwtService;
        _dataContext = dataContext;
    }

    public async Task<TokenResponse> LoginAsync(LoginRequest loginRequest)
    {
        Validators.IsValidEmail(loginRequest.Email);
        
        var user = await _userManager.FindByEmailAsync(loginRequest.Email);

        if (user == null)
        {
            throw new EntityNullException(typeof(User));
        }

        var result = await _userManager.CheckPasswordAsync(user, loginRequest.Password);

        if (!result)
        {
            throw new ValidationException("WRONG PASSWORD");
        }

        var token = await _jwtService.GenerateJwt(user);
        var refreshToken = _jwtService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpires = DateTime.UtcNow.AddDays(7);
        await _dataContext.SaveChangesAsync();

        return new TokenResponse
        {
            Token = token,
            RefreshToken = refreshToken
        };
    }

    public async Task RegisterPatientAsync(RegisterPatientRequest registerPatientRequest)
    {
        Validators.IsValidUser(registerPatientRequest);
        
        if (await _userManager.FindByNameAsync(registerPatientRequest.UserName) != null)
        {
            throw new ValidationException($"USER WITH USERNAME {registerPatientRequest.UserName} EXISTS");
        } else if (await _userManager.FindByEmailAsync(registerPatientRequest.Email) != null)
        {
            throw new ValidationException($"USER WITH EMAIL {registerPatientRequest.Email} EXISTS");
        }
        
        var city = await _dataContext.Cities.FirstOrThrowAsync(c => c.Id == registerPatientRequest.CityId);
        var level = (await _dataContext.Levels.ToListAsync()).FirstOrDefault();
        
        var user = new User
        {
            UserName = registerPatientRequest.UserName,
            Email = registerPatientRequest.Email,
            PhoneNumber = registerPatientRequest.PhoneNumber,
            BloodType = registerPatientRequest.BloodType,
            Gender = registerPatientRequest.Gender,
            GivesBlood = registerPatientRequest.GivesBlood,
            City = city,
            Level = level
        };
        
        await _userManager.CreateAsync(user, registerPatientRequest.Password);
        
        await _userManager.AddToRoleAsync(user, "Patient");
        
        _dataContext.Patients.Add(new Patient { UserId = user.Id });
        
        await _dataContext.SaveChangesAsync();
    }
    
    public async Task RegisterDoctorAsync(RegisterDoctorRequest registerDoctorRequest)
    {
        Validators.IsValidUser(registerDoctorRequest);
        
        if (await _userManager.FindByNameAsync(registerDoctorRequest.UserName) != null)
        {
            throw new Exception($"USER WITH USERNAME {registerDoctorRequest.UserName} EXISTS");
        } else if (await _userManager.FindByEmailAsync(registerDoctorRequest.Email) != null)
        {
            throw new Exception($"USER WITH EMAIL {registerDoctorRequest.Email} EXISTS");
        }
        
        var city = await _dataContext.Cities.FirstOrThrowAsync(c => c.Id == registerDoctorRequest.CityId);
        var level = (await _dataContext.Levels.ToListAsync()).FirstOrDefault();
        
        var user = new User
        {
            UserName = registerDoctorRequest.UserName,
            Email = registerDoctorRequest.Email,
            PhoneNumber = registerDoctorRequest.PhoneNumber,
            BloodType = registerDoctorRequest.BloodType,
            Gender = registerDoctorRequest.Gender,
            GivesBlood = registerDoctorRequest.GivesBlood,
            City = city,
            Level = level
        };

        using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        await _userManager.CreateAsync(user, registerDoctorRequest.Password);

        var doctor = new Doctor {UserId = user.Id, Office = registerDoctorRequest.Office};

        await _dataContext.Doctors.AddAsync(doctor);

        doctor.Educations = registerDoctorRequest.Educations.Select(e => new Education
        {
            Name = e
        }).ToList();

        if (registerDoctorRequest.Specializations.Count <= 0)
        {
            throw new Exception("DOCTOR SHOULD HAVE ATLEAST ONE SPECIALIZATION");
        }

        var specializationSet = new HashSet<int>(registerDoctorRequest.Specializations);

        foreach (var specialization in specializationSet)
        {
            var specializationFetched = await _dataContext.Specializations.FindAsync(specialization);
            if (specializationFetched == null)
            {
                throw new Exception("SPECIALIZATION DOESN'T EXIST");
            }

            doctor.Specializations.Add(specializationFetched);
        }

        if (registerDoctorRequest.Services.Count <= 0)
        {
            throw new Exception("DOCTOR SHOULD HAVE ATLEAST ONE SERVICE");
        }

        var serviceSet = new HashSet<int>(registerDoctorRequest.Services);

        foreach (var service in serviceSet)
        {
            var serviceFetched = await _dataContext.Services.Include(s => s.Specialization).FirstOrDefaultAsync(s => s.Id == service);
                    
            if (serviceFetched == null)
            {
                throw new Exception("SERVICE DOESN'T EXIST");
            }

            if (!specializationSet.Contains(serviceFetched.Specialization.Id))
            {
                throw new Exception("SERVICE IS NOT OF THE SPECIALIZATION U SELECTED");
            }

            doctor.Services.Add(serviceFetched);
        }

        var days = new HashSet<Day>();
                
        foreach (var schedule in registerDoctorRequest.Schedules)
        {
            Validators.IsValidDay(schedule.Day);
                
            if (!days.Add(schedule.Day))
            {
                throw new Exception($"SCHEDULE FOR DAY {schedule.Day} ALREADY EXISTS");
            }

            schedule.Slots.Sort((a, b) => a.StartTime.CompareTo(b.StartTime));
            var slots = schedule.Slots.Select(s => new Slot { StartTime = s.StartTime, EndTime = s.EndTime, ScheduleId = s.ScheduleId }).ToList();

            for (var i = 1; i < slots.Count; i++)
            {
                Validators.IsValidSlot(slots[i - 1], slots[i]);
            }

            doctor.Schedules.Add(new Schedule
            {
                Day = schedule.Day,
                Slots = slots
            });
        }
            
        await _dataContext.SaveChangesAsync();
        await _userManager.AddToRoleAsync(user, "Doctor");
            
        transaction.Complete();
    }
    
    public async Task<IdentityResult> DeleteUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            throw new Exception("USER DOESN'T EXIST");
        }
        
        return await _userManager.DeleteAsync(user);
    }

    public async Task UpdateUserLevelAsync(User user)
    {
        var userFetched = await _dataContext.Users.FirstOrThrowAsync(u => u.Id == user.Id);
        userFetched.Level = user.Level;
        await _dataContext.SaveChangesAsync();
    }
    

    public async Task UpdateUserAsync(string id, UpdateUserRequest updateUserRequest)
    {
        var user = await _dataContext.Users.Include(u => u.City).FirstOrDefaultAsync(u => u.Id == id);
        
        if (user == null)
        {
            throw new Exception("USER DOESN'T EXIST");
        }

        Validators.IsValidUsername(updateUserRequest.UserName);
        Validators.IsValidEmail(updateUserRequest.Email);
        Validators.IsValidPhoneNumber(updateUserRequest.PhoneNumber);
        Validators.IsValidBloodType(updateUserRequest.BloodType);

        user.UserName = updateUserRequest.UserName;
        user.NormalizedUserName = _userManager.NormalizeName(user.UserName);
        user.Email = updateUserRequest.Email;
        user.BloodType = updateUserRequest.BloodType;
        user.NormalizedEmail = _userManager.NormalizeEmail(user.Email);
        user.PhoneNumber = updateUserRequest.PhoneNumber;

        if (updateUserRequest.CityId != user.City.Id)
        {
            var city = await _dataContext.Cities.FirstOrDefaultAsync();

            if (city == null)
            {
                throw new Exception("CITY DOESN'T EXIST");
            }

            user.City = city;
        }

        await _dataContext.SaveChangesAsync();
    }

    public async Task<User> ReadUserAsync(string id)
    {
        return await _dataContext.Users.Include(u => u.City).Include(u => u.Level).FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<UserResponse> ReadMyUserAsync(string id, string role)
    {
        var userFetched = await _dataContext.Users.Include(u => u.City).Include(u => u.Level).FirstOrDefaultAsync(u => u.Id == id);
        
        var userResponse = new UserResponse
        {
            Id = userFetched.Id,
            userName = userFetched.UserName,
            email = userFetched.Email,
            gender = userFetched.Gender,
            Role = role
        };
        
        if (role != "Admin")
        {
            userResponse.phoneNumber = userFetched.PhoneNumber;
            userResponse.BloodType = userFetched.BloodType.Value;
            userResponse.Level = userFetched.Level;
            userResponse.City = userFetched.City;
            userResponse.GivesBlood = userFetched.GivesBlood;
        }
        
        return userResponse;
    }
    
    public async Task<List<User>> ReadPotentialDonorsAsync(BloodType bloodType, int id, string myId)
    {
        return await _dataContext.Users
            .Where(u => u.BloodType == bloodType && u.City.Id == id && u.Id != myId && u.GivesBlood)
            .ToListAsync();
    }
    
    public HashSet<City> ReadPotentialDonorsCitiesAsync(BloodRequest bloodRequest)
    {
        return _userManager.Users
            .Where(u => u.BloodType == bloodRequest.BloodType && u.Id != bloodRequest.User.Id && u.GivesBlood)
            .Select(u => u.City)
            .ToHashSet();
    }

    public async Task<DoctorResponse> ReadDoctorAsync(string id)
    {
        var doctor = await _dataContext.Doctors
            .Include(d => d.User)
            .ThenInclude(u => u.City)
            .Include(d => d.User)
            .ThenInclude(u => u.Level)
            .Include(d => d.Educations)
            .Include(d => d.Schedules)
            .ThenInclude(s => s.Slots)
            .Include(d => d.Services)
            .Include(d => d.Specializations)
            .Include(d => d.Services)
            .FirstOrDefaultAsync(d => d.User.Id == id);

        if (doctor == null)
        {
            throw new Exception("DOCTOR DOESN'T EXIST");
        }

        return new DoctorResponse
        {
            Id = doctor.User.Id,
            userName = doctor.User.UserName!,
            email = doctor.User.Email!,
            phoneNumber = doctor.User.PhoneNumber!,
            gender = doctor.User.Gender,
            BloodType = doctor.User.BloodType.Value,
            XP = doctor.User.xp.Value,
            Level = doctor.User.Level,
            City = doctor.User.City,
            Educations = doctor.Educations,
            Specializations = doctor.Specializations,
            Services = doctor.Services,
            Schedules = doctor.Schedules
        };
    }

    public async Task<List<DoctorSearchDto>> FilterDoctors(FilterDoctorDto filterDto)
    {
        return await _dataContext.Doctors
            .Include(x => x.User)
            .ThenInclude(u => u.City)
            .Include(x => x.Specializations)
            .Include(x => x.Services)
            .Where(p => 
                (filterDto.SearchContent == null || 
                 p.User.UserName.Contains(filterDto.SearchContent) ||
                 p.Services.Any(s => s.Name.Contains(filterDto.SearchContent)) ||
                 p.Specializations.Any(s => s.Name.Contains(filterDto.SearchContent))) &&
                (filterDto.CityId == null || p.User.City.Id == filterDto.CityId))
            .Select(d => new DoctorSearchDto
            {
                Id = d.UserId,
                UserName = d.User.UserName,
                City = d.User.City.Name
            })
            .ToListAsync();
    }
    
    public async Task<string> RefreshToken(string refreshToken)
    {
        return await _jwtService.RefreshToken(refreshToken);
    }
}