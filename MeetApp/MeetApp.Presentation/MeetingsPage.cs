using MeetApp.Application;
using MeetApp.Domain.Entities;
using MeetApp.Domain.Exceptions;
using MeetApp.Presentation.DTO;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace MeetApp.Presentation
{
    public class MeetingsPage : Page
    {
        private readonly IMeetingService _meetingService;
        private List<Meeting> meetings = new();
        private bool editMode = false;
        private int cursor = 0;

        public MeetingsPage(IMeetingService meetingService)
        {
            _meetingService = meetingService;
        }

        public override void Draw()
        {
            if (editMode is false)
            {
                meetings = _meetingService.GetMeetings();
                DisplayAllMeetings();
                if (meetings.Count is 0)
                {
                    ChangePage(PageType.MENU);
                    return;
                }

                var key = Console.ReadKey().Key;

                if (key == ConsoleKey.DownArrow)
                    cursor = Math.Min(cursor + 1, meetings.Count - 1);
                else if (key == ConsoleKey.UpArrow)
                    cursor = Math.Max(0, cursor - 1);
                else if (key == ConsoleKey.Escape)
                    ChangePage(PageType.MENU);
                else if (key == ConsoleKey.Enter)
                    editMode = true;
                else if (key == ConsoleKey.Delete)
                    DeleteMeeting();
            }
            else
            {
                try
                {
                    DisplayChangeOfMeet();
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Ошибка в написании даты.");
                }
                catch (Exception ex)
                {
                    if (ex is DomainException or MeetApp.Application.Exceptions.ApplicationException)
                        Console.WriteLine(ex.Message);
                    else
                        throw;
                }

                Console.WriteLine("Нажмите кнопку");
                Console.ReadKey();

                Clear();

                editMode = false;
            }

            Clear();
        }

        private void DeleteMeeting()
        {
            if (meetings.Count is 0)
                return;

            Clear();
            Console.WriteLine("Вы уверены (Y/N) ?");
            var answ = Console.ReadLine().ToUpper();
            if (answ == "Y")
                _meetingService
                    .DeleteMeeting(meetings.ElementAt(cursor));
        }

        private void DisplayChangeOfMeet()
        {
            Console.SetCursorPosition(0, 0);

            Console.WriteLine($"Изменяется: {MeetingDto.FromMeetings(meetings.ElementAt(cursor))}");
            Console.WriteLine("Изменить время начала встречи (Y/N) ?");
            var answ = Console.ReadLine().ToUpper();
            var meetingToChange = meetings.ElementAt(cursor);
            DateTime start = meetingToChange.MeetingDates.Start;
            DateTime end = meetingToChange.MeetingDates.End;

            if (answ == "Y")
            {
                Console.WriteLine("Введите время начала встречи в формате yyyy-MM-dd:HH-mm");
                var line = Console.ReadLine();
                start = DateTime.ParseExact(line, "yyyy-MM-dd:HH-mm", CultureInfo.InvariantCulture);
            }

            Console.WriteLine("Изменить время конца встречи (Y/N) ?");
            answ = Console.ReadLine().ToUpper();

            if (answ == "Y")
            {
                Console.WriteLine("Введите время конца встречи в формате yyyy-MM-dd:HH-mm");
                var line = Console.ReadLine();
                end = DateTime.ParseExact(line, "yyyy-MM-dd:HH-mm", CultureInfo.InvariantCulture);
            }

            _meetingService.ChangeDatesOfMeeting(meetingToChange, start, end);

            Console.WriteLine("Изменить за сколько до встречи уведомить (Y/N) ?");
            answ = Console.ReadLine().ToUpper();

            if (answ == "Y")
            {
                Console.WriteLine("Введите за сколько времени уведомить в формате дней-часов-минут");
                var values = Console
                    .ReadLine()
                    .Split('-')
                    .Select(Double.Parse)
                    .ToArray();

                if (values.Length == 3)
                {
                    var notifyBefore = TimeSpan.FromDays(values[0])
                    + TimeSpan.FromHours(values[1])
                    + TimeSpan.FromMinutes(values[2]);

                    _meetingService.ChangeNotificationTime(meetings.ElementAt(cursor), notifyBefore);
                }
                else
                {
                    Console.WriteLine("Некорректно введены данные.");
                }
            }
        }

        private void DisplayAllMeetings()
        {
            var meetings = _meetingService.GetMeetings();

            foreach (var (i, meeting) in meetings
                .Select(MeetingDto.FromMeetings)
                .Select((m, i) => (i, m)))
            {
                if (cursor == i)
                    Console.ForegroundColor = ConsoleColor.DarkYellow;

                Console.SetCursorPosition(0, i);
                Console.Write(meeting);

                Console.ResetColor();
            }
        }

        public override void Clear()
        {
            if (editMode)
            {
                var white = new String(' ', Console.BufferWidth);

                for (int i = 0; i < 12; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.Write(white);
                }
            }
            else
            {
                var white = '1' + new String(' ', Console.BufferWidth);

                for (int i = 0; i < meetings.Count; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.Write(white);
                }
                for (int i = 0; i < meetings.Count; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.Write(" ");
                }
            }

        }
    }
}