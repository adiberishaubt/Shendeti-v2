using AutoMapper;
using Shendeti.Domain.Interfaces;
using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;
using Shendeti.Infrastructure.Interfaces;
using Shendeti.Infrastructure.Utils;

namespace Shendeti.Domain.Services;

public class LevelService : ILevelService
{
    private readonly ILevelRepository _levelRepository;
    private readonly IMapper _mapper;

    public LevelService(ILevelRepository levelRepository, IMapper mapper)
    {
        _levelRepository = levelRepository;
        _mapper = mapper;
    }

    public async Task CreateAsync(LevelRequest entity)
    {
        Validators.IsFieldOfLength<Level>(entity.Name, nameof(entity.Name), 3);
        Validators.IsPositiveNumber(entity.RequiredXP, nameof(entity.RequiredXP));

        await _levelRepository.CreateAsync(_mapper.Map<Level>(entity));
    }

    public async Task<Level> ReadAsync(int id)
    {
        return await _levelRepository.ReadAsync(id);
    }

    public async Task<PaginatedList<Level>> ReadAllAsync(int pageIndex, int pageSize)
    {
        return await _levelRepository.ReadAllAsync(pageIndex, pageSize);
    }

    public async Task UpdateAsync(int id, LevelRequest updatedEntity)
    {
        var level = await _levelRepository.ReadAsync(id);
        
        Validators.IsFieldOfLength<Level>(updatedEntity.Name, nameof(updatedEntity.Name), 3);
        Validators.IsPositiveNumber(updatedEntity.RequiredXP, nameof(updatedEntity.RequiredXP));

        level.Name = updatedEntity.Name;
        level.RequiredXP = updatedEntity.RequiredXP;
        
        await _levelRepository.UpdateAsync(level);
    }

    public async Task DeleteAsync(int id)
    {
        var level = await _levelRepository.ReadAsync(id);
        await _levelRepository.DeleteAsync(level);
    }
}