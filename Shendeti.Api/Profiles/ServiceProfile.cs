using AutoMapper;
using Shendeti.Infrastructure.DTOs;
using Shendeti.Infrastructure.Entities;

namespace Shendeti.Profiles;

public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<ServiceRequest, Service>();
    }
}