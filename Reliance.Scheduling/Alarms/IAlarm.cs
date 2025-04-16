namespace Reliance.Scheduling.Alarms
{
	using Reliance.Utility.Metadata;

	/// <summary>
	/// Defines the <see cref="IAlarm" />
	/// </summary>
	public interface IAlarm
	{
		/// <summary>
		/// The IAlarmHandler
		/// </summary>
		/// <returns>The <see cref="RelianceMetadata?"/></returns>
		public delegate RelianceMetadata? IAlarmHandler();

		/// <summary>
		/// Defines the Notifier
		/// </summary>
		public event IAlarmHandler? Notifier;
	}
}
