using System;
using FiroozehGameService.Models.EventArgs;

namespace FiroozehGameService.Handlers
{
    public class DownloadEventHandlers
    {
        public static EventHandler<DownloadProgressArgs> DownloadProgress;
        public static EventHandler<DownloadCompleteArgs> DownloadCompleted;
        public static EventHandler<ErrorArg> DownloadError;
    }
}