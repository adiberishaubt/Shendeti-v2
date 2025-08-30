using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;

namespace Shendeti.Domain.Interfaces;

public interface ISlotsService
{
    Task CreateAsync(SlotRequest entity, string doctorId);
    Task<Slot> ReadAsync(int id);
    Task<List<Slot>> ReadAllAsync();
    Task UpdateAsync(int id, UpdateSlotRequest updatedEntity, string doctorId);
    Task DeleteAsync(int id, string doctorId);
    Task<List<Slot>> ReadDoctorSlots(string id);
}