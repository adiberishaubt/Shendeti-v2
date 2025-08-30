using AutoMapper;
using Shendeti.Domain.Interfaces;
using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;
using Shendeti.Infrastructure.Interfaces;
using Shendeti.Infrastructure.Utils;

namespace Shendeti.Domain.Services;

public class CountryService : ICountryService
{
    private readonly ICountryRepository _countryRepository;
    private readonly IMapper _mapper;

    public CountryService(ICountryRepository countryRepository, IMapper mapper)
    {
        _countryRepository = countryRepository;
        _mapper = mapper;
    }

    public async Task CreateAsync(CountryRequest entity)
    {
        Validators.IsFieldOfLength<Country>(entity.Name, nameof(entity.Name), 3);
        var country = await _countryRepository.ReadAsync(entity.Name);
        Validators.DoesItExist(country, entity.Name, nameof(entity.Name));
        
        await _countryRepository.CreateAsync(_mapper.Map<Country>(entity));
    }

    public async Task<Country> ReadAsync(int id)
    {
        return await _countryRepository.ReadAsync(id);
    }

    public async Task<List<Country>> ReadAllAsync()
    {
        return await _countryRepository.ReadAllAsync();
    }

    public async Task<PaginatedList<Country>> ReadAllAsync(int pageIndex, int pageSize)
    {
        return await _countryRepository.ReadAllAsync(pageIndex, pageSize);
    }

    public async Task UpdateAsync(int id, CountryRequest entity)
    {
        var country = await _countryRepository.ReadAsync(id);
        
        Validators.IsFieldOfLength<Country>(entity.Name, nameof(entity.Name), 3);

        country.Name = entity.Name;

        await _countryRepository.UpdateAsync(country);
    }

    public async Task DeleteAsync(int id)
    {
        var country = await _countryRepository.ReadAsync(id);
        await _countryRepository.DeleteAsync(country);
    }

    public async Task<List<City>> ReadCitiesAsync(int id)
    {
        await _countryRepository.ReadAsync(id);
        return await _countryRepository.ReadCitiesAsync(id);
    }
}