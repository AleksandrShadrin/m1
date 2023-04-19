namespace MeetApp.Domain.Exceptions
{
    public class MeetingTimeIsWrongException : DomainException
    {
        public MeetingTimeIsWrongException(DateTime meetingCreated, DateTime wrongDate)
            : base($"Личная встреча не создана раньше: {meetingCreated}, дата планирования не может быть {wrongDate}")
        {
        }
    }
}
