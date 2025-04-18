namespace Reliance.Scheduling.Alarms
{
	using Reliance.Utility.Metadata;

	public interface IAlarm
	{
		public delegate void IAlarmHandler();

		public event IAlarmHandler? Notifier;
	}
}
