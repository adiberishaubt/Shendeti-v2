using AutoMapper;
using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;

namespace Shendeti.Profiles;

public class CountryProfile : Profile
{
    public CountryProfile()
    {
        CreateMap<CountryRequest, Country>();
    }
}