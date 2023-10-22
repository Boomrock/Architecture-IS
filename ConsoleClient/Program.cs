using Lab1_Architecture_IS.CSVParser;
using Lab1_Architecture_IS.Models;
using Lab1_Architecture_IS;
using NetController;
using NetProtocol;
using System.Net;

namespace ConsoleClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IParser<CSVModel, string> parser = new CSVParser(";");
            IClient<Command> client = new  Client(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8808));
            client.OnReceive += ReceiveEventHandler;
            while(true)
            {
                int chose = ConsoleView.View();
                switch (chose)
                {
                    case 1:
                        client.Send(new Command()
                        {
                            CommandType = CommandType.TransferAll,
                        });
                        break;
                    case 2:
                        {
                            Console.WriteLine("ВВедите номер записи");
                            var str = Console.ReadLine();
                            if (int.TryParse(str, out var result))
                            {
                                client.Send(new Command()
                                {
                                    CommandType = CommandType.TransferByIndex,
                                    Data = new() { { nameof(Int64), result } }
                                });

                            }
                        }
                        break;
                   case 3:
                        {
                            Console.WriteLine("ВВедите номер записи");
                            var str = Console.ReadLine();
                            if (int.TryParse(str, out var result))
                            {
                                client.Send(new Command()
                                {

                                    CommandType = CommandType.Delete,
                                    Data = new() { { nameof(Int64), result } }
                                });

                            }
                        }
                        break;
                        case 4:
                        Console.WriteLine("Введите запись");
                        client.Send(new Command()
                        {
                            CommandType = CommandType.Add,
                            Data = new() {{ nameof(String), Console.ReadLine()}}
                        });
                        break;
                    default:
                        break;
                }
            }
            
        }

        private static void ReceiveEventHandler(Message message)
        {
            Console.WriteLine(message.MessageBody);
        }
    }
}