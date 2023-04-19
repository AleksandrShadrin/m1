using MeetApp.Application;
using MeetApp.Domain.Exceptions;
using System.Globalization;
namespace MeetApp.Presentation
{
    public class CreateMeeting : Page
    {
        private readonly IMeetingService _meetingService;

        public CreateMeeting(IMeetingService meetingService)
        {
            _meetingService = meetingService;
        }

        public override void Draw()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Введите время начала встречи в формате yyyy-MM-dd:HH-mm");
                var line = Console.ReadLine();
                var start = DateTime.ParseExact(line, "yyyy-MM-dd:HH-mm", CultureInfo.InvariantCulture);

                Console.WriteLine("Введите время конца встречи в формате yyyy-MM-dd:HH-mm");
                line = Console.ReadLine();
                var end = DateTime.ParseExact(line, "yyyy-MM-dd:HH-mm", CultureInfo.InvariantCulture);

                Console.WriteLine("Введите за сколько времени уведомить в формате дней-часов-минут");
                var values = Console
                    .ReadLine()
                    .Split('-')
                    .Select(Double.Parse)
                    .ToArray();

                var notifyBefore = TimeSpan.FromDays(values[0])
                    + TimeSpan.FromHours(values[1])
                    + TimeSpan.FromMinutes(values[2]);

                _meetingService
                    .CreateMeeting(start, end, notifyBefore);

            }
            catch (FormatException ex)
            {
                Console.WriteLine("Ошибка в написании даты.");
            }
            catch (Exception ex)
            {
                if(ex is DomainException or MeetApp.Application.Exceptions.ApplicationException)
                    Console.WriteLine(ex.Message);
                else 
                    throw;
            }

            Console.WriteLine("Нажмите любую кнопку");
            Console.ReadKey();

            Clear();

            ChangePage(PageType.MENU);
        }

        public override void Clear()
        {
            var white = new String(' ', Console.BufferWidth);

            for (int i = 0; i < 9; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.WriteLine(white);
            }
        }
    }
}
