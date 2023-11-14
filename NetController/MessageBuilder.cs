using NetProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NetController
{
    public class MessageBuilder
    {
        public static Command BuildMessage(string body, CommandType messageType = CommandType.Add)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(DateTime.Now);
            stringBuilder.Append(":");
            stringBuilder.Append(body);
            JsonDictionary data = new();
            data.Add("Message", stringBuilder.ToString());
            return new Command()
            {
                Data = data,
                CommandType = messageType
            };
        }
        public static Command BuildException(string body,
        [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
        [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
        [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Exception");
            stringBuilder.Append(DateTime.Now);
            stringBuilder.Append(body);
            stringBuilder.Append(memberName);
            stringBuilder.Append(sourceFilePath);
            stringBuilder.Append(sourceLineNumber);
            JsonDictionary data = new();
            data.Add("Error", stringBuilder.ToString());

            return new Command()
            {
                Data = data,
                CommandType = CommandType.Error
            };
        }
        public static Command BuildException(Exception exception)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Exception");
            stringBuilder.Append(DateTime.Now);
            stringBuilder.Append(exception.Message);
            stringBuilder.Append(exception.StackTrace);

            JsonDictionary data = new();
            data.Add("Error", stringBuilder.ToString());

            return new Command()
            {
                Data = data,
                CommandType = CommandType.Error
            };
        }

        public static Command BuildTransferMessage(JsonDictionary data, CommandType messageType = CommandType.TransferByIndex)
        {
            return new Command()
            {
                Data = data,
                CommandType = messageType
            };
        }
    }
}
