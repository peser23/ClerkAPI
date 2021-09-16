using Clerk.Framework.Log.Common.Enums;
using Clerk.Framework.LogHandler.Enums;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Clerk.Framework.LogHandler
{
    public interface ILogger
    {
        Task Error(string error);
        Task Information(string error);
        Task Information(string actiontext, string error);
        Task Error(string actiontext, string error);
        Task Error(string actiontext, Exception ex);
        Task Trace(string error);
        Task InfoLog(object action, object p, string v1, string v2, string className, object MethodName);
        Task ErrorLog(Exception ex, string name1, string name2);
        bool WarningLog(object message, dynamic request = null, string errorCode = "", string identity = "", [CallerFilePath] string fileName = "", [CallerMemberName] string methodName = "", ComponentStatus componentStatus = ComponentStatus.None, LogSeverity logSeverity = LogSeverity.Warn);
    }

}
