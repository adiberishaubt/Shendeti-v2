using AutoMapper;
using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;

namespace Shendeti.Profiles;

public class SpecializationProfile : Profile
{
    public SpecializationProfile()
    {
        CreateMap<SpecializationRequest, Specialization>();
    }
}