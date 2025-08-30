using AutoMapper;
using Shendeti.Domain.Interfaces;
using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;
using Shendeti.Infrastructure.Interfaces;
using Shendeti.Infrastructure.Utils;

namespace Shendeti.Domain.Services;

public class CityService : ICityService
{
    private readonly ICityRepository _cityRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly IMapper _mapper;

    public CityService(ICityRepository cityRepository, ICountryRepository countryRepository, IMapper mapper)
    {
        _cityRepository = cityRepository;
        _countryRepository = countryRepository;
        _mapper = mapper;
    }

    public async Task CreateAsync(CityRequest entity)
    {
        var country = await _countryRepository.ReadAsync(entity.CountryId);
        
        Validators.IsFieldOfLength<City>(entity.Name, nameof(entity.Name), 3);
        Validators.IsValidLongitude(entity.Longitude);
        Validators.IsValidLatitude(entity.Latitude);

        entity.Country = country;

        await _cityRepository.CreateAsync(_mapper.Map<City>(entity));
    }

    public async Task<City> ReadAsync(int id)
    {
        return await _cityRepository.ReadAsync(id);
    }

    public async Task<List<City>> ReadAllAsync()
    {
        return await _cityRepository.ReadAllAsync();
    }

    public async Task<PaginatedList<City>> ReadAllAsync(int pageIndex, int pageSize)
    {
        return await _cityRepository.ReadAllAsync(pageIndex, pageSize);
    }

    public async Task UpdateAsync(int id, CityRequest updatedEntity)
    {
        var city = await _cityRepository.ReadAsync(id);
        var country = await _countryRepository.ReadAsync(updatedEntity.CountryId);
        
        Validators.IsFieldOfLength<Country>(updatedEntity.Name, nameof(updatedEntity.Name), 3);
        Validators.IsValidLongitude(updatedEntity.Longitude);
        Validators.IsValidLatitude(updatedEntity.Latitude);

        city.Name = updatedEntity.Name;
        city.Longitude = updatedEntity.Longitude;
        city.Latitude = updatedEntity.Latitude;
        city.Country = country;

        await _cityRepository.UpdateAsync(city);
    }

    public async Task DeleteAsync(int id)
    {
        var city = await _cityRepository.ReadAsync(id);
        await _cityRepository.DeleteAsync(city);
    }

    public async Task<City> FindNearestCity(BloodRequest bloodRequest, HashSet<City> cities)
    {
        return await _cityRepository.FindNearestCity(bloodRequest, cities);
    }
}