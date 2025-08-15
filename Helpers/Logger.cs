using System;
using System.Diagnostics;
using System.IO;

namespace Exam2.Helpers
{
	public static class Logger
	{
		private static readonly string LogDirectory = @"C:\Logs";
		private static readonly string LogFilePath = Path.Combine(LogDirectory, "ServiceLog.txt");

		static Logger()
		{
			if (!EventLog.SourceExists("Exam2"))
			{
				EventLog.CreateEventSource("Exam2", "Application");
			}

			if (!Directory.Exists(LogDirectory))
			{
				Directory.CreateDirectory(LogDirectory);
			}
		}

		public static void LogInfo(string message)
		{
			Log(message, EventLogEntryType.Information);
		}

		public static void LogError(string message)
		{
			Log(message, EventLogEntryType.Error);
		}

		private static void Log(string message, EventLogEntryType type)
		{
			string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {type} - {message}";

			// Write to Event Viewer
			EventLog.WriteEntry("Exam2", message, type);

			// Rolling log file (max 1MB, archive if needed)
			if (File.Exists(LogFilePath) && new FileInfo(LogFilePath).Length > 1024 * 1024)
			{
				string archivePath = Path.Combine(LogDirectory, $"ServiceLog_{DateTime.Now:yyyyMMddHHmmss}.txt");
				File.Move(LogFilePath, archivePath);
			}

			File.AppendAllText(LogFilePath, logMessage + Environment.NewLine);
		}
	}
}
