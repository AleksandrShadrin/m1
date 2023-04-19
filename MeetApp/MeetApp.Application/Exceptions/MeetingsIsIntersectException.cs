namespace MeetApp.Application.Exceptions
{
    public class MeetingsIsIntersectException : ApplicationException
    {
        public MeetingsIsIntersectException() :
            base("Одна или более встреча пересекаются с данной.")
        {
        }
    }
}
