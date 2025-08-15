using System;
using System.IO;
using System.ServiceProcess;
using System.Threading;
using Exam2.Helpers;

namespace Exam2.Services
{
	public class FileMoverService : ServiceBase
	{
		private FileSystemWatcher _watcher;
		private readonly string _sourcePath = @"C:\Folder1"; 
		private readonly string _destinationPath = @"C:\Folder2";

		public FileMoverService()
		{
			ServiceName = "Exam2";
		}

		protected override void OnStart(string[] args)
		{
			try
			{
				if (!Directory.Exists(_sourcePath))
					Directory.CreateDirectory(_sourcePath);

				if (!Directory.Exists(_destinationPath))
					Directory.CreateDirectory(_destinationPath);

				_watcher = new FileSystemWatcher(_sourcePath)
				{
					Filter = "*.*",
					EnableRaisingEvents = true,
					NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite
				};

				_watcher.Created += OnFileCreated;

				Logger.LogInfo("Service started successfully.");
			}
			catch (Exception ex)
			{
				Logger.LogError($"OnStart error: {ex.Message}");
				throw;
			}
		}

		protected override void OnStop()
		{
			_watcher?.Dispose();
			Logger.LogInfo("Service stopped.");
		}

		private void OnFileCreated(object sender, FileSystemEventArgs e)
		{
			try
			{
				// Wait until the file is completely ready
				if (WaitForFile(e.FullPath, 10))
				{
					string destFile = Path.Combine(_destinationPath, Path.GetFileName(e.FullPath));
					File.Move(e.FullPath, destFile);
					Logger.LogInfo($"File moved successfully: {e.Name}");
				}
				else
				{
					Logger.LogError($"File not ready after waiting: {e.Name}");
				}
			}
			catch (Exception ex)
			{
				Logger.LogError($"File move failed: {ex.Message}");
			}
		}

		/// <summary>
		/// Waits for a file to be ready for reading (not locked by another process).
		/// </summary>
		private bool WaitForFile(string filePath, int maxRetries)
		{
			for (int i = 0; i < maxRetries; i++)
			{
				try
				{
					using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
					{
						if (stream.Length > 0)
							return true; // File is ready
					}
				}
				catch (IOException)
				{
					Thread.Sleep(1000); // wait 1 second before retry
				}
			}
			return false; // File was never ready within retry limit
		}
	}
}
