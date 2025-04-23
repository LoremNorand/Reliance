namespace Reliance.Logging.Interfaces
{
	public interface ILogFormatter
	{
		public string Format(ILogMessage message);
	}
}
