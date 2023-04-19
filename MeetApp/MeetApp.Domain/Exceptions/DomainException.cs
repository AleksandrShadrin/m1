namespace MeetApp.Domain.Exceptions
{
    abstract public class DomainException : Exception
    {
        protected DomainException(string message)
            : base(message)
        { }
    }
}
