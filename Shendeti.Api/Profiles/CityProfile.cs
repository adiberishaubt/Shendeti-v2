using AutoMapper;
using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;

namespace Shendeti.Profiles;

public class CityProfile : Profile
{
    public CityProfile()
    {
        CreateMap<CityRequest, City>();
    }
}