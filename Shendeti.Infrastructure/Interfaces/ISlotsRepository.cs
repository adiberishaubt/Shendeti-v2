using Shendeti.Infrastructure.Entities;

namespace Shendeti.Infrastructure.Interfaces;

public interface ISlotsRepository
{
    Task CreateAsync(Slot entity);
    Task<Slot> ReadAsync(int id);
    Task<List<Slot>> ReadAllAsync();
    Task UpdateAsync(Slot updatedEntity);  
    Task DeleteAsync(Slot entity);
    Task<List<Slot>> ReadDoctorSlots(string id);
}