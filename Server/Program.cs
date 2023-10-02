using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using NetProtocol;
using Lab1_Architecture_IS.Models;
using System.IO;
using System.Diagnostics;
using Lab1_Architecture_IS;
using Lab1_Architecture_IS.;
using Lab1_Architecture_IS.CSVParser;

namespace Server
{
    internal class Program
    {
        static Queue<Command<CSVModel>> commands = new Queue<Command<CSVModel>>();
        static IEditor<CSVModel> editor;

        static void Main(string[] args)
        {

            
            var csvFileController = new CSVFileController<CSVModel>("./file.csv", new CSVParser(";"));
            editor= new CSVEditor<CSVModel>(csvFileController);

            using var udpServer = new UdpClient(5555);
            Console.WriteLine("UDP-сервер запущен...");
            Receive(udpServer);
            

            
            Console.ReadLine();


        }
        static async Task Receive(UdpClient udpServer)
        {
            while (true)
            {
                var result = await udpServer.ReceiveAsync();
                Console.WriteLine($"{result}");
                var JsonString = Encoding.UTF8.GetString(result.Buffer);
                var command = JsonSerializer
                    .Deserialize <Command<string>>(JsonString);
                _ = Task.Factory.StartNew(() => ExecuteCommand(command, result.RemoteEndPoint)); 
            }
        }

        private static void ExecuteCommand(Command<string> command, System.Net.IPEndPoint remoteEndPoint)
        {
            Message message;
            switch (command.Type)
            {
                case CommandType.SaveData:
                    var model =  JsonSerializer.Deserialize<CSVModel>(command.Data);
                    if (model != null)
                    {
                        editor.Add(model);
                    }

                    break;
                case CommandType.GetData: 
                    break;
                case CommandType.GetAllData:
                    break;
                case CommandType.DeleteData:
                    break;

            }
        }
    }
}