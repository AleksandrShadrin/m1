using MeetApp.Application;
using MeetApp.Presentation.DTO;

namespace MeetApp.Presentation.services
{
    public class MeetingsExporter
    {
        private readonly IMeetingService _meetingService;
        public MeetingsExporter(IMeetingService meetingService)
        {
            _meetingService = meetingService;
        }

        public bool TryExportMeetings(string path)
        {
            var meetings = _meetingService
                .GetMeetings()
                .Select(MeetingDto.FromMeetings)
                .Select(m => m.ToString());

            if (string.IsNullOrWhiteSpace(path))
            {
                return false;
            }

            if (Directory.Exists(Path.GetDirectoryName(path)) is false)
            {
                try
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                }
                catch (Exception)
                {
                    return false;
                }
            }

            using var file = File.Create(path);
            using var streamWriter = new StreamWriter(file);

            foreach (var meeting in meetings)
            {
                streamWriter.WriteLine(meeting);
            }

            return true;
        }
    }
}
