using System.IO;

namespace BL
{
    public class FileInfo
    {
        public string FilePath { get; set; }

        public WatcherChangeTypes WatcherChangeType { get; set; }


        public FileInfo(string filePath, WatcherChangeTypes watcherChangeType)
        {
            this.FilePath = filePath;
            this.WatcherChangeType = watcherChangeType;
        }
    }
}
