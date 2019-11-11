namespace FiroozehGameService.Models.EventArgs
{
    public class DownloadProgressArgs:System.EventArgs
    {
        public long ProgessSize;
        public long TotalSize;
        public byte[] data;
    }
}