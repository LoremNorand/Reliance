namespace Reliance.Logging.Implementations
{
	using Reliance.Core.Interfaces;
	using Reliance.Logging.Implementations.ActionListener;
	using Reliance.Logging.Interfaces;
	using Reliance.Utility.Metadata;
	using System.Globalization;

	public class LoggingManager
	{
		private static LoggingManager _instance = new();

		private readonly List<IActionListener<Metadata>> _actionListeners = new();
		private Dictionary<string, ILoggerNode<string>?> _loggers = new();

		private LoggingManager()
		{
			_loggers["__Metadata_listening"] = new LoggerNode();
			_actionListeners.Add(new MetadataCreatedActionListener());
			Metadata.MetadataCreated += _actionListeners[0].OnAction;
		}

		public ILoggerNode<string>? this[string key]
		{
			get => _loggers[key];
			set => _loggers[key] = value;
		}

		public static LoggingManager Instance
		{
			get { return _instance ?? new LoggingManager(); }
		}
	}
}
