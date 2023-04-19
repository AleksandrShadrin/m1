using MeetApp.Application;
using MeetApp.Domain.Entities;
using MeetApp.Presentation.DTO;
using System.Diagnostics;

namespace MeetApp.Presentation.services
{
    public class MeetingNotificationService
    {
        private readonly IMeetingRepository _meetRepository;
        public event Action<List<MeetingDto>> OnNotification;

        public MeetingNotificationService(IMeetingRepository meetRepository)
        {
            _meetRepository = meetRepository;
        }

        public Task StartNotification()
        {
            CheckForNotify();
            return Task.CompletedTask;
        }

        async Task CheckForNotify()
        {
            while (true)
            {
                var meetings = await _meetRepository
                    .GetMeetings();

                var toNotify = meetings
                    .Where(m => m.NotificationTime.NotifyAt - DateTime.Now >= TimeSpan.Zero)
                    .Where(m =>
                        m.NotificationTime.NotifyAt - DateTime.Now <= TimeSpan.FromMinutes(10))
                    .Select(MeetingDto.FromMeetings).ToList();

                if (toNotify.Count is not 0)
                    OnNotification?.Invoke(toNotify.ToList());

                await Task
                    .Delay(TimeSpan.FromMinutes(5));
            }
        }
    }
}