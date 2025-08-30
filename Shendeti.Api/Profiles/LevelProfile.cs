using AutoMapper;
using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;

namespace Shendeti.Profiles;

public class LevelProfile : Profile
{
    public LevelProfile()
    {
        CreateMap<LevelRequest, Level>();
    }
}