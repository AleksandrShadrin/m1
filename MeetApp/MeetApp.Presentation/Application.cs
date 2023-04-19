using MeetApp.Presentation.DTO;
using MeetApp.Presentation.services;
using Microsoft.Extensions.DependencyInjection;

namespace MeetApp.Presentation
{
    public partial class Application
    {
        private readonly IServiceProvider _provider;
        private readonly MeetingNotificationService _notificationService;

        private Page currentPage = new Menu();
        private PageType currentPageType = PageType.MENU;
        private bool notify = false;
        private List<MeetingDto> meetings;
        private object drawLock = new object();

        public Application(MeetingNotificationService notificationService, IServiceProvider provider)
        {
            _notificationService = notificationService;
            _provider = provider;
        }

        public async Task Run()
        {
            currentPage.pageChanges = OnPageChanged;
            await _notificationService.StartNotification();
            _notificationService.OnNotification += OnNotificationRequested;

            while (currentPageType != PageType.EXIT)
            {
                await Task.Run(() =>
                {
                    lock (drawLock)
                    {
                        lock (drawLock)
                        {
                            currentPage.Draw();
                        }
                    }
                });

                await Task.Delay(50);
            }
        }

        private void OnPageChanged(PageType type)
        {
            currentPageType = type;
            if (type is PageType.EXIT)
                return;

            currentPage = GetPage(type);

            currentPage.pageChanges = OnPageChanged;
        }

        private Page GetPage(PageType type)
            => type switch
            {
                PageType.MENU => _provider.GetRequiredService<Menu>(),
                PageType.MEETING_CREATION => _provider.GetRequiredService<CreateMeeting>(),
                PageType.MEETINGS_WATCH => _provider.GetRequiredService<MeetingsPage>(),
                PageType.MEETINGS_EXPORT => _provider.GetRequiredService<MeetingsExportPage>(),
            };

        private void OnNotificationRequested(List<MeetingDto> meetings)
        {
            notify = true;
            this.meetings = meetings;

            var currentPos = Console.GetCursorPosition();
            DisplayNotification();
            Console.SetCursorPosition(currentPos.Left, currentPos.Top);
            notify = false;
        }

        private void ClearNotification(int n)
        {
            var height = Console.WindowHeight;
            var white = new String(' ', 100);

            for (int i = 0; i <= n; i++)
            {
                Console.SetCursorPosition(0, height - i - 2);
                Console.WriteLine(white);
            }
        }

        private void DisplayNotification()
        {
            lock (drawLock)
            {
                lock (drawLock)
                {
                    var height = Console.WindowHeight;

                    Console.SetCursorPosition(0, height - meetings.Count - 2);
                    meetings.ForEach(Console.WriteLine);
                }
            }

            var n = meetings.Count;

            Task.Run(async () =>
            {
                await Task.Delay(2000);
                lock (drawLock)
                {
                    lock (drawLock)
                    {
                        ClearNotification(n);
                    }
                }
            });
        }
    }
}