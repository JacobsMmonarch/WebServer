using System;
using System.Collections.Generic;
using System.IO;

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
            fileSystemWatcher = new FileSystemWatcher();
            fileSystemWatcher.Path = path;

            fileSystemWatcher.Created += OnFileCreated;
            fileSystemWatcher.Deleted += OnFileDeleted;
            fileSystemWatcher.Changed += OnFileChanged;
            fileSystemWatcher.Renamed += OnFileRenamed;

            fileSystemWatcher.EnableRaisingEvents = true;
        }

        private void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            results.Add($"File created: {e.FullPath}");
            FileCreated?.Invoke(sender, e);
        }

        private void OnFileDeleted(object sender, FileSystemEventArgs e)
        {
            results.Add($"File deleted: {e.FullPath}");
            FileDeleted?.Invoke(sender, e);
        }

        private void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            results.Add($"File changed: {e.FullPath}");
            FileChanged?.Invoke(sender, e);
        }

        private void OnFileRenamed(object sender, RenamedEventArgs e)
        {
            results.Add($"File renamed: {e.OldFullPath} to {e.FullPath}");
            FileRenamed?.Invoke(sender, e);
        }

        public List<string> GetResults()
        {
            return results;
        }

        public void StopMonitoring()
        {
            fileSystemWatcher.EnableRaisingEvents = false;
        }
    }
}