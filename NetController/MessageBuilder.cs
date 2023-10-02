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
        public static Message BuildMessage(string body, MessageType messageType)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(DateTime.Now);
            stringBuilder.Append(":");
            stringBuilder.Append(body);

            return new Message()
            {
                MessageBody = stringBuilder.ToString(),
                MessageType = messageType
            };
        }
        public static Message BuildException(string body,
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

            return new Message()
            {
                MessageBody = stringBuilder.ToString(),
                MessageType = MessageType.Error
            };
        }
        public static Message BuildException(Exception exception)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Exception");
            stringBuilder.Append(DateTime.Now);
            stringBuilder.Append(exception.Message);
            stringBuilder.Append(exception.StackTrace);

            return new Message()
            {
                MessageBody = stringBuilder.ToString(),
                MessageType = MessageType.Error
            };
        }

    }
}
