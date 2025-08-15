using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace Exam2.Installers
{
	[RunInstaller(true)]
	public class ProjectInstaller : Installer
	{
		private ServiceInstaller serviceInstaller;
		private ServiceProcessInstaller processInstaller;

		public ProjectInstaller()
		{
			processInstaller = new ServiceProcessInstaller
			{
				Account = ServiceAccount.LocalService
			};

			serviceInstaller = new ServiceInstaller
			{
				ServiceName = "Exam2",
				DisplayName = "Exam2 File Mover Service, Moves File from Folder 1 to Folder 2 and Logs Results",
				StartType = ServiceStartMode.Automatic
			};

			Installers.Add(processInstaller);
			Installers.Add(serviceInstaller);
		}
	}
}
