using Lab1_Architecture_IS.Models;
using Lab1_Architecture_IS.CSVParser;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Lab1_Architecture_IS
{
    public class ConsoleView
    {
        private static IEditor<CSVModel> editor;
        private static IParser<CSVModel, string> parser;
        private static string delimiters = ";";
        static void Start()
        {
            parser = new CSVParser.CSVParser(delimiters);
            var path = "./a.csv";
            var csvFileController = new CSVFileController<CSVModel>(path, new CSVParser.CSVParser(delimiters));
            editor = new CSVEditor<CSVModel>(csvFileController);

            // Цикл меню
            while (true)
            {
                // Очистить консоль и вывести опции меню
                Console.Clear();
                Console.WriteLine("Выберите одну из следующих опций:");
                Console.WriteLine("1) Вывод всех записей на экран");
                Console.WriteLine("2) Вывод записи по номеру");
                Console.WriteLine("3) Запись данных в файл");
                Console.WriteLine("4) Удаление записи (записей) из файла");
                Console.WriteLine("5) Добавление записи в файл");

                // Обработать ввод пользователя в зависимости от выбранной опции
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.D1:
                        // Вывод всех записей на экран
                        PrintAllRecords(path);
                        break;
                    case ConsoleKey.D2:
                        // Вывод записи по номеру
                        PrintRecordByNumber(path);
                        break;
                    case ConsoleKey.D3:
                        // Запись данных в файл
                        UpdateDataToFile(path);
                        break;
                    case ConsoleKey.D4:
                        // Удаление записи (записей) из файла
                        DeleteRecordFromFile(path);
                        break;
                    case ConsoleKey.D5:
                        // Добавление записи в файл
                        AddRecordToFile(path);
                        break;
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;
                    default:
                        // Неверный ввод
                        Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте еще раз.");
                        break;
                }
                Console.WriteLine("Нажмите любую клавишу, чтобы продолжить...");
                Console.ReadKey();
            }

        }

        // Метод для вывода всех записей на экран
        static void PrintAllRecords(string filePath)
        {
            var lines = editor.ReadAll();
            for (int i = 0; i < lines.Length; i++)
            {
                Console.WriteLine($"{i + 1})" + printModel(lines[i]));
            }
        }

        private static string printModel(CSVModel model)
        {
            if (model == null) return "";
            return $" {model.Id} {model.Name} {model.Type} {model.IsInteractive} {model.Volume}";
        }

        // Метод для вывода записи по номеру
        static void PrintRecordByNumber(string filePath)
        {
            Console.Write("Введите номер записи: ");
            int number = Int16.Parse(Console.ReadLine());
            // Вывести запись по номеру на экран
            Console.WriteLine($"Запись {number}: " +
                printModel(editor.Read(number - 1)));
        }

        // Метод для записи данных в файл
        static void UpdateDataToFile(string filePath)
        {
            // Попросить пользователя ввести данные, которые он хочет записать в файл
            Console.Write("Введите данные, которые вы хотите записать в файл" +
                "(int Id;string Name;string Type;bool IsInteractiv;int Volume): ");
            string data = Console.ReadLine();
            Console.Write("Введите номер строки: ");
            int index = Int16.Parse(Console.ReadLine());

            var model = parser.Parse(data);
            if (model == null)
            {
                Console.WriteLine("Не правильный ввод данных");
                return;
            }
            editor.Update(model, index - 1);
        }

        // Метод для удаления записи (записей) из файла
        static void DeleteRecordFromFile(string filePath)
        {

            // Попросить пользователя ввести номера записей, которые он хочет удалить из файла, разделенные запятыми
            Console.Write("Введите номера записей, которые вы хотите удалить из файла, разделенные запятыми: ");
            string input = Console.ReadLine();

            // Разбить ввод на массив подстрок по запятым
            string[] numbers = input.Split(',');

            // Создать список для хранения номеров записей, которые нужно удалить
            List<int> indexes = new List<int>();

            // Перебрать все подстроки и проверить, являются ли они числами и находятся ли они в диапазоне от 1 до количества строк в файле
            foreach (string number in numbers)
            {
                if (int.TryParse(number, out int index))
                {
                    // Добавить номер записи в список, уменьшив его на единицу для соответствия индексации списка
                    indexes.Add(index - 1);
                }
                else
                {
                    // Неверный ввод
                    Console.WriteLine($"Неверный номер записи: {number}");
                }
            }

            // Сортировать список номеров по убыванию, чтобы удаление не нарушало индексацию списка строк
            indexes.Sort();
            indexes.Reverse();

            // Перебрать все номера и удалить соответствующие строки из списка
            foreach (int index in indexes)
            {
                editor.Delete(index);
            }

            // Записать обновленный список строк в файл, перезаписывая его содержимое
            Console.WriteLine("Записи успешно удалены из файла.");

        }


        // Метод для добавления записи в файл
        static void AddRecordToFile(string filePath)
        {
            // Попросить пользователя ввести данные, которые он хочет добавить в файл
            Console.Write("Введите данные, которые вы хотите записать в файл" +
                "(int Id;string Name;string Type;bool IsInteractiv;int Volume): ");
            string data = Console.ReadLine();

            // Проверить, не пустая ли строка
            if (!string.IsNullOrEmpty(data))
            {
                var model = parser.Parse(data);
                if (model == null)
                {
                    Console.WriteLine("Не правильный ввод данных");
                    return;
                }
                // Добавить данные в файл с новой строки
                editor.Add(model);
                Console.WriteLine("Данные успешно добавлены в файл.");
            }
            else
            {
                // Пустая строка
                Console.WriteLine("Нет данных для добавления в файл.");
            }
        }

    }
}

