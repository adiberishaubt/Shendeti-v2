using Shendeti.Domain.Configurations;
using Shendeti.Domain.Interfaces;
using Shendeti.Domain.Services;
using Shendeti.Infrastructure.Interfaces;
using Shendeti.Infrastructure.Repositories;
using Shendeti.Infrastructure.Services;
using Shendeti.Middlewares;

namespace Shendeti.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<ExceptionMiddleware>();
        serviceCollection.AddTransient<IJwtService, JwtService>();
        serviceCollection.AddTransient<IUserService, UserService>();
        serviceCollection.AddTransient<IMailService, MailService>();
        serviceCollection.AddTransient<IMeetingService, MeetingService>();
        serviceCollection.AddTransient<IBloodRequestService, BloodRequestService>();
        serviceCollection.AddTransient<ILevelService, LevelService>();
        serviceCollection.AddTransient<ICountryService, CountryService>();
        serviceCollection.AddTransient<ICityService, CityService>();
        serviceCollection.AddTransient<ISpecializationService, SpecializationService>();
        serviceCollection.AddTransient<IServiceService, ServiceService>();
        serviceCollection.AddTransient<ISlotsService, SlotsService>();
        serviceCollection.AddTransient<IScheduleService, ScheduleService>();
        serviceCollection.AddTransient<IAppointmentService, AppointmentService>();
        serviceCollection.AddTransient<IEducationService, EducationService>();
        serviceCollection.AddHttpClient();
    }
    
    public static void AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<ICountryRepository, CountryRepository>();
        serviceCollection.AddTransient<IEducationRepository, EducationRepository>();
        serviceCollection.AddTransient<ICityRepository, CityRepository>();
        serviceCollection.AddTransient<ISpecializationRepository, SpecializationRepository>();
        serviceCollection.AddTransient<IServiceRepository, ServiceRepository>();
        serviceCollection.AddTransient<ILevelRepository, LevelRepository>();
        serviceCollection.AddTransient<IScheduleRepository, ScheduleRepository>();
        serviceCollection.AddTransient<ISlotsRepository, SlotsRepository>();
        serviceCollection.AddTransient<IAppointmentRepository, AppointmentRepository>();
        serviceCollection.AddTransient<IBloodRequestRepository, BloodRequestRepository>();
    }
    
    public static void AddConfigurations(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.Configure<MailSettings>(configuration.GetSection(nameof(MailSettings)));
    }
}