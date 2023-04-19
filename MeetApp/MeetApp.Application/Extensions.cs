using Microsoft.Extensions.DependencyInjection;

namespace MeetApp.Application
{
    public static class Extensions
    {
        public static IServiceCollection RegisterApplication(this IServiceCollection services)
        {
            services.AddSingleton<IMeetingService, MeetingService>();

            return services;
        }
    }
}
