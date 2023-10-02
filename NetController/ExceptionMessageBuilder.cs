using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetController
{
    internal class ExceptionMessageBuilder
    {
        public static string BuildMessageException(string body, 
        [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
        [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
        [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            StringBuilder stringBuilder= new StringBuilder();
            stringBuilder.AppendLine("Exception");
            stringBuilder.Append(DateTime.Now);
            stringBuilder.Append(body);
            stringBuilder.Append(memberName);
            stringBuilder.Append(sourceFilePath);
            stringBuilder.Append(sourceLineNumber);
            return stringBuilder.ToString();
        }

    }
}
