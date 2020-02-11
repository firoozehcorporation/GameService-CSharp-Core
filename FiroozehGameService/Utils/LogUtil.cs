using System;

namespace FiroozehGameService.Utils
{
    public enum LogType
    {
       Normal , Error
    }

    public class Log
    {
        internal Log(LogType type, string txt)
        {
            Type = type;
            Txt = txt;
        }

        public LogType Type { get; }
        public string Txt { get; }
    }
        
    public class LogUtil
    {
        private const bool IsDebug = false;
        public static EventHandler<Log> LogEventHandler;


        internal static void Log(object where,string txt)
        {
           if(IsDebug)
           LogEventHandler?.Invoke(where,new Log(LogType.Normal,DateTime.Now.ToString("h:mm:ss tt")+ "--" + txt));
        }
        internal static void LogError(object where,string err)
        {
            if(IsDebug)
            LogEventHandler?.Invoke(where,new Log(LogType.Error,DateTime.Now.ToString("h:mm:ss tt")+ "--" + err));
        }
    }
}