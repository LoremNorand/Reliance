using Reliance.Logging.Interfaces;

namespace Reliance.Logging.Implementations.LogOutput
{
	public class LogConsoleOutput : ILogOutputChannel<string>
	{
		public void Post(string message)
		{
			Console.WriteLine(message);
		}
	}
}
