using MeetApp.Application;
using MeetApp.Domain.Entities;

namespace MeetApp.Infrastructure.InMemory
{
    public class InMemoryMeetingRepository : IMeetingRepository
    {
        private readonly List<Meeting> _meetings = new();

        public Task AddMeeting(Meeting meeting)
        {
            _meetings.Add(meeting);
            return Task.CompletedTask;
        }

        public Task DeleteMeeting(Meeting meeting)
        {
            _meetings.Remove(meeting);
            return Task.CompletedTask;
        }

        public Task<List<Meeting>> GetMeetings()
            => Task.FromResult(_meetings);

        public Task UpdateMeeting(Meeting meeting)
        {
            var idx = _meetings
                .FindIndex(m => m.MeetingCreated == meeting.MeetingCreated);

            _meetings[idx] = meeting;

            return Task.CompletedTask;
        }
    }
}
