using Reliance.Utility.Metadata;

namespace Reliance.Scheduling.Alarms
{
	internal class StampAlarm : BaseAlarm
	{
		public override event IAlarm.IAlarmHandler? Notifier;

		protected DateTime _timeStamp;

		public StampAlarm(string name, DateTime timeStamp) : base(name)
		{
			_timeStamp = timeStamp;
		}

		protected override Metadata? RaiseEvent(Metadata? __metadata = null)
		{
			Notifier?.Invoke();

			return new Metadata(this,
				["Событие в BaseAlarm"],
				MetadataStatus.Success);
		}

		public StampAlarm AtSecond(double seconds)
		{
			return this;
		}

		public StampAlarm AtYear(double year)
		{
			return this;
		}

		public StampAlarm AtMonth(double month)
		{
			return this;
		}

		public StampAlarm AtHour(double hour)
		{
			return this;
		}

		public StampAlarm AtMinute(double minute)
		{
			return this;
		}

		public StampAlarm AtMillisecond(double millisecond)
		{
			return this;
		}

		#region Два события

		protected override async Task RunAsync(CancellationToken cancellationToken)
		{
			while(!cancellationToken.IsCancellationRequested)
			{
				try
				{
					RaiseEvent();
				}
				catch(TaskCanceledException)
				{
					break;
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex.ToString());
					break;
				}
			}
		}

		#endregion
	}
}
