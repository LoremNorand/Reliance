using Reliance.Utility.Metadata;

namespace Reliance.Scheduling.Alarms
{
	public class AlarmVault
	{
		private static readonly AlarmVault _instance = new();

		private Dictionary<string, BaseAlarm> _alarms = new();
		public static AlarmVault Instance => _instance ?? new();
		private AlarmVault() { }

		public BaseAlarm this[string key]
		{
			get => _alarms[key];
			set => _alarms[key] = value;
		}

		public bool IsOccupied(string key)
		{
			return _alarms.ContainsKey(key);
		}
	}
}
