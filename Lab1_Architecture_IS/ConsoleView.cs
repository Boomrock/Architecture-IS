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
        public static int View()
        {
            Console.Clear();
            Console.WriteLine("Выберите одну из следующих опций:");
            Console.WriteLine("1) Вывод всех записей на экран");
            Console.WriteLine("2) Вывод записи по номеру");
            Console.WriteLine("3) Удаление записи из файла");
            Console.WriteLine("4) Добавление записи в файл");
            return Console.Read();
        }

    
    }
}

