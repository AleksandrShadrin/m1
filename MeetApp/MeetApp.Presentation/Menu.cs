namespace MeetApp.Presentation
{
    public class Menu : Page
    {
        private int cursor = 0;

        public override void Draw()
        {
            OutputToConsole();
            ConsoleKey key = Console.ReadKey().Key;

            if (key == ConsoleKey.Escape)
                ChangePage(PageType.EXIT);

            if (key == ConsoleKey.DownArrow)
                cursor = Math.Min(cursor + 1, 2);
            else if (key == ConsoleKey.UpArrow)
                cursor = Math.Max(0, cursor - 1);
            else if (key == ConsoleKey.Enter)
                ChangePage(CursorOn());
            else
                return;

            Clear();
        }

        private void OutputToConsole()
        {
            if (cursor is 0)
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(0,0);
            Console.WriteLine("Встречи");
            Console.ResetColor();
            if (cursor is 1)
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(0, 1);
            Console.WriteLine("Назначить встречу");
            Console.ResetColor();
            if (cursor is 2)
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("Экспортировать встречи");
            Console.ResetColor();
        }

        public override void Clear()
        {
            var white = new String(' ', 100);

            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.WriteLine(white);
            }
        }

        private PageType CursorOn()
            => cursor switch
            {
                0 => PageType.MEETINGS_WATCH,
                1 => PageType.MEETING_CREATION,
                2 => PageType.MEETINGS_EXPORT
            };
    }
}
