using MeetApp.Application;
using MeetApp.Infrastructure.InMemory;
using Microsoft.Extensions.DependencyInjection;

namespace MeetApp.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection RegisterInMemoryRepository(this IServiceCollection services)
        {
            services.AddSingleton<IMeetingRepository, InMemoryMeetingRepository>();
            return services;
        }
    }
}
