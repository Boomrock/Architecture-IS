using Lab1_Architecture_IS;
using Lab1_Architecture_IS.CSVParser;
using Lab1_Architecture_IS.Models;
using Lab1_Architecture_IS.DataBase;
using NetController;
using NetProtocol;
using NLog;
using System.Text;

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
                    editor.Add(
                       parser.Parse(GetValue<string>(command.Data)));
                    Logger.Info(command.Data);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    return MessageBuilder.BuildException(ex);
                    throw;
                }
                return MessageBuilder.BuildMessage($"model added");

            });
            server.AddRoute(CommandType.Delete, (command) =>
            {
                try
                {
                    var deleteItem = editor.Delete((int)GetValue<Int64>(command.Data));
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
                    var index = GetValue<Int64>(command.Data);
                    var model = editor.Read((int)index);
                    return MessageBuilder.BuildTransferMessage(parser.Parse(model));
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
                    for (int i = 0; i < model.Length; i++)
                    {
                        stringBuilder.AppendLine(parser.Parse(model[i]));
                    }
                    if(model != null)
                    {
                        return MessageBuilder.BuildTransferMessage(stringBuilder.ToString());
                    }
                    else
                    {
                        return MessageBuilder.BuildException("Out of range array");
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    return MessageBuilder.BuildException(ex);
                    throw;
                }
                return MessageBuilder.BuildMessage($"model Delete");

            });
            server.Start();

            Console.ReadLine();
            server.Stop();
            editor.Close();

        }
        static T GetValue<T>(Dictionary<string, object> dictionary)
        {

            Type type = typeof(T);
            if (dictionary.TryGetValue(type.Name, out var @object))
            {
                return (T)@object;
            }
            else
            {
                throw new Exception($"Key {nameof(T)} doesn't contains in dictionary");
            }

            return default(T);
        }
    } 
}