namespace Reliance.Scheduling.Alarms
{
	using Reliance.Utility.Metadata;

	public interface IAlarm
	{
		public delegate Metadata? IAlarmHandler();

		public event IAlarmHandler? Notifier;
	}
}
