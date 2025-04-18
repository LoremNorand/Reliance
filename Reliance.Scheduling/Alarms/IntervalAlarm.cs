using Reliance.Utility.Metadata;

namespace Reliance.Scheduling.Alarms
{
	public class IntervalAlarm : BaseAlarm
	{
		#region Protected Fields

		protected TimeSpan _interval;

		#endregion Protected Fields



		#region Public Constructors

		public IntervalAlarm(string name) : base(name)
		{
			_interval = TimeSpan.FromSeconds(60);
		}

		public IntervalAlarm(string name, TimeSpan interval) : base(name)
		{
			_interval = interval;
		}

		#endregion Public Constructors



		#region Public Events

		public override event IAlarm.IAlarmHandler? Notifier;

		#endregion Public Events



		#region Protected Methods

		protected override Metadata? RaiseEvent(Metadata? __metadata = null)
		{
			Notifier?.Invoke();

			return new Metadata(this,
				["Событие в IntervalAlarm", $"Интервал: {_interval.Seconds}"],
				MetadataStatus.Success);
		}

		protected override async Task RunAsync(CancellationToken cancellationToken)
		{
			while(!cancellationToken.IsCancellationRequested)
			{
				try
				{
					await Task.Delay(_interval, cancellationToken);
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

		#endregion Protected Methods
	}
}