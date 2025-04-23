using Reliance.Utility.Metadata;

namespace Reliance.Scheduling.Alarms
{
	public class AlarmStorage
	{
		#region Private Fields

		private static readonly AlarmStorage _instance = new();

		private Dictionary<string, BaseAlarm> _alarms = new();

		#endregion Private Fields



		#region Private Constructors

		private AlarmStorage()
		{ }

		#endregion Private Constructors



		#region Public Properties

		public static AlarmStorage Instance => _instance ?? new();

		#endregion Public Properties



		#region Public Indexers

		public BaseAlarm this[string key]
		{
			get => _alarms[key];
			set => _alarms[key] = value;
		}

		#endregion Public Indexers



		#region Public Methods

		public bool IsOccupied(string key)
		{
			return _alarms.ContainsKey(key);
		}

		#endregion Public Methods
	}
}