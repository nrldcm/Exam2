using System.ServiceProcess;
using Exam2.Services;

namespace Exam2
{
	static class Program
	{
		static void Main()
		{
			ServiceBase.Run(new FileMoverService());
		}
	}
}
