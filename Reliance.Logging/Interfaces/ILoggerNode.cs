namespace Reliance.Logging.Interfaces
{
	public interface ILoggerNode<T>
	{
		#region Public Properties

		public int Level { get; }
		public string Name { get; }
		public ILoggerNode<T>? Parent { get; }
		public ILogFormatter Formatter { get; }
		public ILogOutputChannel<T> OutputChannel { get; }
		public void Log(ILogMessage message);

		#endregion Public Properties
	}
}