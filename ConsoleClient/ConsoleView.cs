namespace ConsoleClient
{
    public class ConsoleView
    {
        public static int View()
        {
            Console.Clear();
            Console.WriteLine("Выберите одну из следующих опций:");
            Console.WriteLine("1) Вывод всех записей на экран");
            Console.WriteLine("2) Вывод записи по номеру");
            Console.WriteLine("3) Удаление записи из файла");
            Console.WriteLine("4) Добавление записи в файл");
            var str = Console.ReadLine();
            if (int.TryParse(str, out var result))
            {
                return result;
            }
            return 0;
        }

    
    }
}

