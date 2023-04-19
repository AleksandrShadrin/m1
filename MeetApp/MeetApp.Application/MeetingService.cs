using MeetApp.Application.Exceptions;
using MeetApp.Domain.Entities;
using MeetApp.Domain.ValueObjects;

namespace MeetApp.Application
{
    public class MeetingService : IMeetingService
    {
        private readonly IMeetingRepository _meetRepository;

        public MeetingService(IMeetingRepository meetRepository)
        {
            _meetRepository = meetRepository;
        }

        public void ChangeDatesOfMeeting(Meeting meeting, 
            DateTime startOfMeeting, 
            DateTime endOfMeeting)
        {
            meeting
                .ChangeMeetingDates(startOfMeeting, endOfMeeting);

            CheckIfIntersect(meeting.MeetingDates);

            _meetRepository
                .UpdateMeeting(meeting)
                .Wait();
        }

        public void ChangeNotificationTime(Meeting meeting, TimeSpan notifyBefore)
        {
            meeting
                .ChangeNotificationTime(notifyBefore);

            _meetRepository.UpdateMeeting(meeting).Wait();
        }

        public void CreateMeeting(DateTime startOfMeeting, DateTime endOfMeeting, TimeSpan notifyBefore)
        {
            var meetingCreated = new MeetingCreated(DateTime.Now);

            var meetingNotificationTime = new MeetingNotificationTime(startOfMeeting,
                notifyBefore);

            var meetingDates = MeetingDates.CreateMeetingDates(startOfMeeting,
                endOfMeeting);

            CheckIfIntersect(meetingDates);

            var meeting = new Meeting(meetingCreated,
                meetingDates,
                meetingNotificationTime);

            _meetRepository
                .AddMeeting(meeting)
                .Wait();
        }

        public void DeleteMeeting(Meeting meeting)
            => _meetRepository
                .DeleteMeeting(meeting)
                .Wait();

        public List<Meeting> GetMeetings()
        {
            var task = _meetRepository.GetMeetings();
            task.Wait();

            return task.Result;
        }

        private void CheckIfIntersect(MeetingDates meetingDates)
        {
            if (GetMeetings()
                .Any(m => m.MeetingDates.IntersectWith(meetingDates)))
                throw new MeetingsIsIntersectException();
        }
    }
}