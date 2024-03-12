namespace WebServer.Models
{
    public class Monitoring
    {
        private FileSystemWatcher fileSystemWatcher;
        private List<string> results = new List<string>();

        public event EventHandler<FileSystemEventArgs> FileCreated;
        public event EventHandler<FileSystemEventArgs> FileDeleted;
        public event EventHandler<FileSystemEventArgs> FileChanged;
        public event EventHandler<RenamedEventArgs> FileRenamed;

        public Monitoring(string path)
        {
            // Инициализация FileSystemWatcher
            fileSystemWatcher = new FileSystemWatcher();
            fileSystemWatcher.Path = path;

            // Установка обработчиков событий
            fileSystemWatcher.Created += OnFileCreated;
            fileSystemWatcher.Deleted += OnFileDeleted;
            fileSystemWatcher.Changed += OnFileChanged;
            fileSystemWatcher.Renamed += OnFileRenamed;

            // Включение мониторинга
            fileSystemWatcher.EnableRaisingEvents = true;
        }

        private async void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            results.Add($"File created: {e.FullPath}");
            FileCreated?.Invoke(sender, e);
            await Task.Delay(0); // Ожидание для асинхронной обработки
        }
        private async void OnFileDeleted(object sender, FileSystemEventArgs e)
        {
            results.Add($"File created: {e.FullPath}");
            FileDeleted?.Invoke(sender, e);
            await Task.Delay(0); // Ожидание для асинхронной обработки
        }
        private async void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            results.Add($"File changed: {e.FullPath}");
            FileChanged?.Invoke(sender, e);
            await Task.Delay(0); // Ожидание для асинхронной обработки
        }
        private async void OnFileRenamed(object sender, RenamedEventArgs e)
        {
            results.Add($"File renamed: {e.OldFullPath} to {e.FullPath}");
            FileRenamed?.Invoke(sender, e);
            await Task.Delay(0); // Ожидание для асинхронной обработки
        }

        // Метод для получения текущих результатов
        public List<string> GetResults()
        {
            return results;
        }

        // Метод для остановки мониторинга
        public void StopMonitoring()
        {
            fileSystemWatcher.EnableRaisingEvents = false;
        }
    }
}