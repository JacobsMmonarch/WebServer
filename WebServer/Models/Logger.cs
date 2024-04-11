using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

public class ServerLogger
{
    private string logFilePath;
    private string[] serverIPAddresses;

    public ServerLogger(string filePath, IConfiguration configuration)
    {
        logFilePath = filePath;
        serverIPAddresses = configuration?["ServerIPAddresses"]?.Split(',') ?? new string[0];
    }

    public async Task LogResponseTimeAsync()
    {
        try
        {
            using (StreamWriter writer = File.AppendText(logFilePath))
            {
                DateTime startTime = DateTime.Now;
                HttpClient client = new HttpClient();
                foreach (var serverIPAddress in serverIPAddresses)
                {
                    string url = $"https://{serverIPAddress}/";
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                }
                TimeSpan responseTime = DateTime.Now - startTime;
                string logMessage = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} - Response Time: {responseTime.TotalMilliseconds} ms";
                await writer.WriteLineAsync(logMessage);
                Console.WriteLine("Connected successfully.");
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error connecting to the server: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing to log file: {ex.Message}");
        }
    }

    private void WriteToLogFile(string message)
    {
        try
        {
            using (StreamWriter writer = File.AppendText(logFilePath))
            {
                writer.WriteLine(message);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing to log file: {ex.Message}");
        }
    }
}

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

public class MonitoringHub : Hub
{
    public async Task SendResult(string result)
    {
        await Clients.All.SendAsync("ReceiveResult", result);
    }
}
