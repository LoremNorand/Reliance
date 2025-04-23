using Reliance.Logging.Interfaces;

namespace Reliance.Logging.Implementations.LogOutput
{
	public class LogConsoleOutput : ILogOutputChannel
	{
		public void Post<T>(T message)
		{
			Console.WriteLine(message?.ToString());
		}
	}
}
