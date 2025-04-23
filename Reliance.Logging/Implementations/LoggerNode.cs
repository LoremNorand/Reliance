using Reliance.Logging.Implementations.LogHandling;
using Reliance.Logging.Implementations.LogOutput;
using Reliance.Logging.Interfaces;

namespace Reliance.Logging.Implementations
{
	public class LoggerNode : ILoggerNode<string>
	{
		#region Private Fields

		private ILogFormatter _formatter = new UnicodeLogFormatter();
		private int _level = 0;
		private string _name = "Безымянный узел";
		private ILogOutputChannel<string> _outputChannel = new LogConsoleOutput();
		private ILoggerNode<string>? _parent;

		#endregion Private Fields



		#region Public Properties

		public ILogFormatter Formatter
		{
			get => _formatter;
			set => _formatter = value;
		}

		public int Level
		{
			get => _level;
			protected set => _level = value;
		}

		public string Name
		{
			get => _name;
			set => _name = value;
		}

		public ILogOutputChannel<string> OutputChannel
		{
			get => _outputChannel;
			set => _outputChannel = value;
		}

		public ILoggerNode<string>? Parent
		{
			get => _parent;
			set
			{
				_parent = value;
				ParentChanged();
			}
		}

		#endregion Public Properties



		#region Public Methods

		public void Log(ILogMessage message)
		{
			message.Components.Add("__Level", _level.ToString());
			string formattedMessage = _formatter.Format(message);
			_outputChannel.Post(formattedMessage);
		}

		#endregion Public Methods



		#region Private Methods

		private void ParentChanged()
		{
			_level = (_parent != null) ? _parent.Level + 1 : 0;
		}

		#endregion Private Methods
	}
}