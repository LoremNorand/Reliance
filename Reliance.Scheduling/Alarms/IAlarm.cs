namespace Reliance.Scheduling.Alarms
{
	using Reliance.Utility.Metadata;

	public interface IAlarm
	{
		public delegate RelianceMetadata? IAlarmHandler();

		public event IAlarmHandler? Notifier;
	}
}
