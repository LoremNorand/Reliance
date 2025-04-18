using Reliance.Utility.Metadata;

namespace Reliance.Scheduling.Alarms
{
	public class AlarmVault
	{
		#region Private Fields

		private static readonly AlarmVault _instance = new();

		private Dictionary<string, BaseAlarm> _alarms = new();

		#endregion Private Fields



		#region Private Constructors

		private AlarmVault()
		{ }

		#endregion Private Constructors



		#region Public Properties

		public static AlarmVault Instance => _instance ?? new();

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