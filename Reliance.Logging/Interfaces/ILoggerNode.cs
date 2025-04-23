namespace Reliance.Logging.Interfaces
{
	public interface ILoggerNode
	{
		#region Public Properties

		public int Level { get; }
		public string Name { get; }
		public ILoggerNode? Parent { get; }
		public ILogFormatter Formatter { get; }
		public ILogOutputChannel OutputChannel { get; }
		public void Log(ILogMessage message);

		#endregion Public Properties
	}
}