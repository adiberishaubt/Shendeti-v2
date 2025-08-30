using AutoMapper;
using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;

namespace Shendeti.Profiles;

public class BloodRequestProfile : Profile
{
    public BloodRequestProfile()
    {
        CreateMap<BloodRequestDto, BloodRequest>();
    }
}