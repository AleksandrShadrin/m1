using MeetApp.Domain.Exceptions;

namespace MeetApp.Domain.ValueObjects
{
    public record MeetingDates
    {
        public DateTime Start { get; private init; }
        public DateTime End { get; private init; }

        public static MeetingDates CreateMeetingDates(DateTime start, DateTime end)
        {
            if (start >= end)
            {
                throw new StartOfMeetingIsLessThanEndException(start, end);
            }

            if (start <= DateTime.Now)
            {
                throw new MeetingTimeIsWrongException(DateTime.Now, start);
            }

            return new(start, end);
        }

        public static MeetingDates RestoreMeetingDates(DateTime start, DateTime end)
        {
            return new(start, end);
        }

        internal MeetingDates(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public bool IntersectWith(MeetingDates meetingDates)
            => (meetingDates.Start > End || meetingDates.End < Start) is false;
    }
}
