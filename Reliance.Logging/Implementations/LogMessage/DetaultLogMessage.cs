using Reliance.Logging.Interfaces;

namespace Reliance.Logging.Implementations.LogMessage
{
	public class DetaultLogMessage : ILogMessage
	{
		#region Private Fields

		private Dictionary<string, string> _components = new();

		#endregion Private Fields

		#region Public Constructors

		public DetaultLogMessage(Dictionary<string, string>? components = null)
		{
			if(components != null)
			{
				_components = components;
			}
		}

		#endregion Public Constructors



		#region Public Properties

		public Dictionary<string, string> Components
		{
			get => _components;
			set => _components = value;
		}

		#endregion Public Properties
	}
}