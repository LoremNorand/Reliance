namespace Reliance.Scheduling.Alarms
{
	public interface IAlarm
	{
		#region Public Delegates

		public delegate void IAlarmHandler();

		#endregion Public Delegates



		#region Public Events

		public event IAlarmHandler? Notifier;

		#endregion Public Events
	}
}