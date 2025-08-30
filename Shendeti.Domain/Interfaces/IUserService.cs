using Microsoft.AspNetCore.Identity;
using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;
using Shendeti.Infrastructure.Utils;

namespace Shendeti.Domain.Interfaces;

public interface IUserService
{
    Task<TokenResponse> LoginAsync(LoginRequest loginRequest);
    Task RegisterPatientAsync(RegisterPatientRequest registerPatientRequest);
    Task RegisterDoctorAsync(RegisterDoctorRequest registerDoctorRequest);
    Task<string> RefreshToken(string refreshToken);
    Task<IdentityResult> DeleteUser(string id);
    Task UpdateUserAsync(string id, UpdateUserRequest updateUserRequest);
    Task<User> ReadUserAsync(string id);
    Task<UserResponse> ReadMyUserAsync(string id, string role);
    Task<List<User>> ReadPotentialDonorsAsync(BloodType bloodType, int id, string myId);
    Task<DoctorResponse> ReadDoctorAsync(string id);
    HashSet<City> ReadPotentialDonorsCitiesAsync(BloodRequest bloodRequest);
    Task UpdateUserLevelAsync(User user);
    Task<List<DoctorSearchDto>> FilterDoctors(FilterDoctorDto filterDto);
}