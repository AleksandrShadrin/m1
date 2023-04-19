namespace MeetApp.Domain.ValueObjects
{
    public record MeetingNotificationTime
    {
        public DateTime NotifyAt { get; private init; }

        public MeetingNotificationTime(DateTime start, 
            TimeSpan notifyBefore)
        {
            NotifyAt = start - notifyBefore;
        }
    }
}
