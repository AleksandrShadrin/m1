using MeetApp.Domain.Entities;

namespace MeetApp.Application
{
    public interface IMeetingService
    {
        void CreateMeeting(DateTime startOfMeeting, DateTime endOfMeeting, TimeSpan notifyBefore);
        List<Meeting> GetMeetings();
        void ChangeDatesOfMeeting(Meeting meeting, DateTime startOfMeeting, DateTime endOfMeeting);
        void ChangeNotificationTime(Meeting meeting, TimeSpan notifyBefore);
        void DeleteMeeting(Meeting meeting);
    }
}