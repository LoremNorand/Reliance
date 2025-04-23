using Reliance.Logging.Implementations.LogHandling;
using Reliance.Logging.Implementations.LogOutput;
using Reliance.Logging.Interfaces;
using Reliance.Core.Storage;

namespace Reliance.Logging.Implementations
{
	public class LoggerNode : BaseStorageComponent, ILoggerNode
	{
		#region Private Fields

		private ILogFormatter _formatter = new UnicodeLogFormatter();
		private int _level = 0;
		private string _name = "Безымянный узел";
		private ILogOutputChannel _outputChannel = new LogConsoleOutput();
		private ILoggerNode? _parent;

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

		public override string Name
		{
			get => _name;
		}

		public ILogOutputChannel OutputChannel
		{
			get => _outputChannel;
			set => _outputChannel = value;
		}

		public ILoggerNode? Parent
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