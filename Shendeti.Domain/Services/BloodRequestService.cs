using AutoMapper;
using Shendeti.Domain.Interfaces;
using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;
using Shendeti.Infrastructure.Exceptions;
using Shendeti.Infrastructure.Interfaces;
using Shendeti.Infrastructure.Utils;

namespace Shendeti.Domain.Services;

public class BloodRequestService : IBloodRequestService
{
    private readonly ICityService _cityService;
    private readonly IBloodRequestRepository _bloodRequestRepository;
    private readonly IUserService _userService;
    private readonly IMailService _mailService;
    private readonly ILevelService _levelService;
    private readonly IMapper _mapper;

    public BloodRequestService(IBloodRequestRepository bloodRequestRepository, IUserService userService, IMailService mailService, ICityService cityService, ILevelService levelService, IMapper mapper)
    {
        _bloodRequestRepository = bloodRequestRepository;
        _userService = userService;
        _mailService = mailService;
        _cityService = cityService;
        _levelService = levelService;
        _mapper = mapper;
    }

    public async Task CreateAsync(BloodRequestDto bloodRequest, string id)
    {
        var city = await _cityService.ReadAsync(bloodRequest.CityId);
        
        Validators.IsValidStatus(bloodRequest.Status);

        var bloodRequestEntity = _mapper.Map<BloodRequest>(bloodRequest);

        bloodRequestEntity.User = await _userService.ReadUserAsync(id);
        bloodRequestEntity.City = city;
        bloodRequestEntity.DateTime = DateTime.Now;
        
        await _bloodRequestRepository.CreateAsync(bloodRequestEntity);
    }

    public async Task<BloodRequest> ReadAsync(int id)
    {
        return await _bloodRequestRepository.ReadAsync(id);
    }

    public async Task<List<BloodRequest>> ReadAllAsync()
    {
        return await _bloodRequestRepository.ReadAllAsync();
    }

    public async Task UpdateAsync(int id, BloodRequest entity)
    {
        var bloodRequest = await _bloodRequestRepository.ReadAsync(id);
        bloodRequest.Offset = entity.Offset;
        bloodRequest.BloodType = entity.BloodType;
        bloodRequest.City = entity.City;
        bloodRequest.MailSent = entity.MailSent;
        bloodRequest.Spitali = entity.Spitali;
        bloodRequest.Urgent = entity.Urgent;
        bloodRequest.User = entity.User;
        bloodRequest.DateTime = entity.DateTime;

        await _bloodRequestRepository.UpdateAsync(bloodRequest);
    }

    public async Task DeleteAsync(int id, string patientId)
    {
        var bloodRequest = await _bloodRequestRepository.ReadAsync(id);
        if (bloodRequest.User.Id != patientId) throw new ValidationException($"Blood Request does not belong to user with id {patientId}");
        await _bloodRequestRepository.DeleteAsync(bloodRequest);
    }

    private async Task<List<PotentialDonor>> ReadDonors(BloodRequest bloodRequest, City city)
    {
        return (await _userService.ReadPotentialDonorsAsync(bloodRequest.BloodType, city.Id, bloodRequest.User.Id))
            .Select(u => new PotentialDonor
            {
                RequestId = bloodRequest.Id,
                UserId = u.Id,
                Name = u.UserName!,
                Email = u.Email!,
                BloodType = bloodRequest.BloodType,
                CityName = bloodRequest.City.Name,
                Spitali = bloodRequest.Spitali,
                Urgent = bloodRequest.Urgent
            }).ToList();
    }

    private async Task ProcessRequest(BloodRequest bloodRequest)
    {
        var donors = new List<PotentialDonor>();

        if (bloodRequest.Offset == 0) 
        {
            donors = await ReadDonors(bloodRequest, bloodRequest.City);
        }
        
        if (donors.Count <= 0)
        {
            var cities = _userService.ReadPotentialDonorsCitiesAsync(bloodRequest);

            if (cities.Count <= bloodRequest.Offset && !bloodRequest.MailSent)  
            {
                await _mailService.SendNoDonorFoundMailAsync(bloodRequest);
                bloodRequest.MailSent = true;
                bloodRequest.DateTime = DateTime.Now;
                await UpdateAsync(bloodRequest.Id, bloodRequest);
                return;
            }
            
            if (cities.Count > bloodRequest.Offset)
            {
                var nearestCity = await _cityService.FindNearestCity(bloodRequest, cities);
            
                donors = await ReadDonors(bloodRequest, nearestCity);
            }
            else
            {
                return;
            }
        }

        foreach (var donor in donors)
        {
            await _mailService.SendRequestMailAsync(donor);
        }

        if (bloodRequest.Offset == 0)
        {
            bloodRequest.Status = Status.WAITING;
            bloodRequest.Offset++;
        }
        
        bloodRequest.DateTime = DateTime.Now;
        await UpdateAsync(bloodRequest.Id, bloodRequest);
    }

    public async Task ProcessRequestsAsync()
    {
        var requests = await _bloodRequestRepository.ReadRequestsByStatusAsync(Status.NOT_COMPLETED);

        foreach (var request in requests)
        {
            await ProcessRequest(request);
        }
        
    }

    public async Task AcceptRequestAsync(int id, string donorId)
    {
        var request = await _bloodRequestRepository.ReadAsync(id);
        
        var donor = await _userService.ReadUserAsync(donorId);
        
        await _mailService.SendCompletedRequestMailAsync(request, donor);

        request.Status = Status.WAITING_COMPLETION;

        await UpdateAsync(id, request);
    }

    public async Task ConfirmRequestCompletionAsync(int id, string donorId)
    {
        var request = await _bloodRequestRepository.ReadAsync(id);
        var donor = await _userService.ReadUserAsync(donorId);
        
        request.Status = Status.COMPLETED;

        if (request.Urgent)
            donor.xp += 100;
        else
            donor.xp += 50;
        
        var nextLevel = await _levelService.ReadAsync(donor.Level.Id+1);
        
        if (donor.xp >= nextLevel.RequiredXP)
        {
            donor.Level = nextLevel;
        }

        await _userService.UpdateUserLevelAsync(donor);
        await UpdateAsync(id, request);
    }
    
    public async Task ProcessWaitingRequestsAsync()
    {
        var requests = await _bloodRequestRepository.ReadWaitingRequestsAsync();
        
        foreach (var request in requests)
        {
            await ProcessRequest(request);
        }
        
    }
}