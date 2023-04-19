using MeetApp.Presentation.services;

namespace MeetApp.Presentation
{
    public class MeetingsExportPage : Page
    {
        private readonly MeetingsExporter _exporter;

        public MeetingsExportPage(MeetingsExporter exporter)
        {
            _exporter = exporter;
        }

        public override void Clear()
        {
            var white = new String(' ', 100);
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(white);
            Console.SetCursorPosition(0, 1);
            Console.WriteLine(white);
            Console.SetCursorPosition(0, 2);
            Console.WriteLine(white);
        }

        public override void Draw()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Напишите путь к файлу, куда экспортировать данные");

            var path = Console.ReadLine();

            if (_exporter.TryExportMeetings(path) is false)
            {
                Console.WriteLine("Экспорт не удался.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Экспорт удался.");
                Console.ReadKey();
            }

            Clear();

            ChangePage(PageType.MENU);
        }
    }
}
