using AutoMapper;
using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;

namespace Shendeti.Profiles;

public class SlotProfile : Profile
{
    public SlotProfile()
    {
        CreateMap<SlotRequest, Slot>();
    }
}