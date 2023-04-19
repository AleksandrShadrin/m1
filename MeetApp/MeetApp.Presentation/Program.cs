using MeetApp.Application;
using MeetApp.Infrastructure;
using MeetApp.Presentation;
using MeetApp.Presentation.services;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.RegisterApplication();
services.RegisterInMemoryRepository();
services.AddSingleton<MeetingNotificationService>();
services.AddSingleton<Application>();
services.AddSingleton<CreateMeeting>();
services.AddSingleton<MeetingsPage>();
services.AddSingleton<Menu>();
services.AddSingleton<MeetingsExporter>();
services.AddSingleton<MeetingsExportPage>();

var provider = services.BuildServiceProvider();
var app = provider.GetRequiredService<Application>();

await app.Run();
