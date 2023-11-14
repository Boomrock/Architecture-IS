using Lab1_Architecture_IS;
using Lab1_Architecture_IS.CSVParser;
using Lab1_Architecture_IS.Models;
using Lab1_Architecture_IS.DataBase;
using NetController;
using NetProtocol;
using NLog;
using System.Text;

using System.Text.Json.Nodes;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

            IParser<CSVModel, string> parser = new CSVParser(";");
            IEditor<CSVModel> editor = new DataBaseEditor(new DataBaseContext());

            IServer<Command, CommandType> server = new NetController.Server();

            server.AddRoute(CommandType.Add, (command) =>
            {
                try
                {
                    var model = command.Data.Get<CSVModel>("Model");
                    var csvModel = model as CSVModel;
                    editor.Add(csvModel);

  
                    var models = editor.ReadAll();
                    JsonDictionary data = new();
                    data.Add("Models", models);
                    return MessageBuilder.BuildTransferMessage(data, CommandType.TransferAll);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    return MessageBuilder.BuildException(ex);
                    throw;
                }

            });
            server.AddRoute(CommandType.Delete, (command) =>
            {
                try
                {
                    var deleteItem = editor.Delete(command.Data.Get<int>("Id"));
                    return MessageBuilder.BuildMessage(parser.Parse(deleteItem) + " has been deleted");
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    return MessageBuilder.BuildException(ex);
                    throw;
                }

            });
            server.AddRoute(CommandType.TransferByIndex, (command) =>
            {
                try
                {
                    var index = command.Data.Get<int>("Id");
                    var model = editor.Read(index);
                    JsonDictionary data = new();
                    data.Add("Model", model);
                    return MessageBuilder.BuildTransferMessage(data);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    return MessageBuilder.BuildException(ex);
                    throw;
                }

            });
            server.AddRoute(CommandType.TransferAll, (command) =>
            {
                try
                {
                    var model = editor.ReadAll();
                    var stringBuilder = new StringBuilder();
                    JsonDictionary data = new();
                    data.Add("Models", model);
                    return MessageBuilder.BuildTransferMessage(data, CommandType.TransferAll);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    return MessageBuilder.BuildException(ex);

                }
            });
            server.Start();

            Console.ReadLine();
            server.Stop();
            editor.Close();

        }
    } 
}