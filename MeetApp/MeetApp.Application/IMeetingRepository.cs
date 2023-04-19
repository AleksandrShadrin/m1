using MeetApp.Domain.Entities;

namespace MeetApp.Application
{
    public interface IMeetingRepository
    {
        Task AddMeeting(Meeting meeting);
        Task<List<Meeting>> GetMeetings();
        Task UpdateMeeting(Meeting meeting);
        Task DeleteMeeting(Meeting meeting);
    }
}
