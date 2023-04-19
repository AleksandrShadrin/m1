using MeetApp.Domain.Entities;

namespace MeetApp.Presentation.DTO
{
    public class MeetingDto
    {
        public DateTime CreatedAt { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime NotifyAt { get; set; }

        public static MeetingDto FromMeetings(Meeting meeting) 
            => new MeetingDto()
            {
                CreatedAt = meeting.MeetingCreated.AtTime,
                End = meeting.MeetingDates.End,
                Start = meeting.MeetingDates.Start,
                NotifyAt = meeting.NotificationTime.NotifyAt
            };

        public override string ToString()
            => $"Создано: {CreatedAt}, начало {Start}, Конец {End}";
    }
}
