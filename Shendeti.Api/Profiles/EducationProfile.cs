using AutoMapper;
using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;

namespace Shendeti.Profiles;

public class EducationProfile : Profile
{
    public EducationProfile()
    {
        CreateMap<EducationRequest, Education>();
    }
}