namespace FiroozehGameService.Models.Command
{
    public class DownloadProgressArgs
    {
        public long ProgessSize;
        public long TotalSize;
        public byte[] data;
    }
}