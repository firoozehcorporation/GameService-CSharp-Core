using System;
using FiroozehGameService.Models.EventArgs;

namespace FiroozehGameService.Handlers
{
    public class DownloadEventHandlers
    {
        public static EventHandler<DownloadProgressArgs> DownloadProgress;
        public static EventHandler DownloadCompleted;
        public static EventHandler<ErrorArg> DownloadError;
    }
}