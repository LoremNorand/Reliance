namespace Reliance.Logging.Interfaces
{
	public interface ILogMessageBuilder
	{
		public ILogMessageBuilder Add((string key, string value) keyValuePair);
		public ILogMessageBuilder Add(KeyValuePair<string, string> keyValuePair);
		public ILogMessage Build();
	}
}
