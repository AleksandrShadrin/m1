namespace MeetApp.Domain.Exceptions
{
    public class StartOfMeetingIsLessThanEndException : DomainException
    {
        public StartOfMeetingIsLessThanEndException(DateTime start, DateTime end) 
            : base($"Начало встречи: {start} не может начинаться раньше конца: {end}")
        {
        }
    }
}
