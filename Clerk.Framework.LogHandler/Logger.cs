using Clerk.Framework.Log.Common.Enums;
using Clerk.Framework.LogHandler.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Clerk.Framework.LogHandler
{
    public class Logger //: ILogHandler
    {
        private static string _serviceType;
        private static bool _logging;
        private static string _applicationCode;

        public Logger()
        {
        }

        public bool ErrorLog(Exception exception, [CallerFilePath] string fileName = "", [CallerMemberName] string methodName = "", ComponentStatus componentStatus = ComponentStatus.None, LogSeverity logSeverity = LogSeverity.Error)
        {
            return true;
        }

        public bool ErrorLog(object message, Exception exception, dynamic request = null, string errorCode = "", string identity = "", [CallerFilePath] string fileName = "", [CallerMemberName] string methodName = "", ComponentStatus componentStatus = ComponentStatus.None, LogSeverity logSeverity = LogSeverity.Error)
        {
            return true;
        }

        public bool FatalLog(object message, Exception exception, dynamic request = null, string errorCode = "", string identity = "", [CallerFilePath] string fileName = "", [CallerMemberName] string methodName = "", ComponentStatus componentStatus = ComponentStatus.None, LogSeverity logSeverity = LogSeverity.Fatal)
        {
            return true;
        }

        public bool DebugLog(object message, dynamic request = null, string errorCode = "", string identity = "", [CallerFilePath] string fileName = "", [CallerMemberName] string methodName = "", ComponentStatus componentStatus = ComponentStatus.None, LogSeverity logSeverity = LogSeverity.Debug)
        {
            return true;
        }

        public bool WarningLog(object message, dynamic request = null, string errorCode = "", string identity = "", [CallerFilePath] string fileName = "", [CallerMemberName] string methodName = "", ComponentStatus componentStatus = ComponentStatus.None, LogSeverity logSeverity = LogSeverity.Warn)
        {
            return true;
        }

        public bool InfoLog(object message, dynamic request = null, string errorCode = "", string identity = "", [CallerFilePath] string fileName = "", [CallerMemberName] string methodName = "", ComponentStatus componentStatus = ComponentStatus.None, LogSeverity logSeverity = LogSeverity.Info)
        {
            return true;
        }
    }
}