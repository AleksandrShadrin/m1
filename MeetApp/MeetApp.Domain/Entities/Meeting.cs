using MeetApp.Domain.ValueObjects;

namespace MeetApp.Domain.Entities
{
    public class Meeting
    {
        public MeetingCreated MeetingCreated { get; private set; }
        public MeetingDates MeetingDates { get; private set; }
        public MeetingNotificationTime NotificationTime { get; private set; }

        public Meeting(MeetingCreated meetingCreated,
            MeetingDates meetingDates,
            MeetingNotificationTime notificationTime)
        {
            MeetingCreated = meetingCreated;
            MeetingDates = meetingDates;
            NotificationTime = notificationTime;
        }

        public void ChangeMeetingDates(DateTime start, DateTime end)
            => MeetingDates = new(start, end);

        public void ChangeNotificationTime(TimeSpan notifyBefore)
            => NotificationTime = new(MeetingDates.Start, notifyBefore);
    }
}
