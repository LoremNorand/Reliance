using Reliance.Utility.Metadata;

namespace Reliance.Scheduling.Alarms
{
	public class StampAlarm : BaseAlarm
	{
		#region Protected Fields

		protected double? _day = null;
		protected double? _hour = null;
		protected bool _isTimeBinded = false;
		protected double? _millisecond = null;
		protected double? _minute = null;
		protected double? _month = null;
		protected double? _second = null;
		protected double? _year = null;

		#endregion Protected Fields

		#region Private Fields

		// Флаг, чтобы событие вызывалось только один раз при захвате времени
		private bool _hasFired = false;

		#endregion Private Fields

		#region Public Constructors

		public StampAlarm(string name) : base(name)
		{ }

		#endregion Public Constructors



		#region Public Events

		public override event IAlarm.IAlarmHandler? Notifier;

		#endregion Public Events



		#region Public Methods

		public StampAlarm AtHour(double hour)
		{
			_hour = hour;
			_isTimeBinded = true;
			return this;
		}

		public StampAlarm AtMillisecond(double millisecond)
		{
			_millisecond = millisecond;
			_isTimeBinded = true;
			return this;
		}

		public StampAlarm AtMinute(double minute)
		{
			_minute = minute;
			_isTimeBinded = true;
			return this;
		}

		public StampAlarm AtMonth(double month)
		{
			_month = month;
			_isTimeBinded = true;
			return this;
		}

		public StampAlarm AtSecond(double second)
		{
			_second = second;
			_isTimeBinded = true;
			return this;
		}

		public StampAlarm AtYear(double year)
		{
			_year = year;
			_isTimeBinded = true;
			return this;
		}

		#endregion Public Methods



		#region Protected Methods

		protected bool IsTimeCatched()
		{
			bool match = true;
			DateTime now = DateTime.Now;
			match = (_year == null) ? match : (match && now.Year == _year);
			match = (_month == null) ? match : (match && now.Month == _month);
			match = (_day == null) ? match : (match && now.Day == _day);
			match = (_hour == null) ? match : (match && now.Hour == _hour);
			match = (_minute == null) ? match : (match && now.Minute == _minute);
			match = (_second == null) ? match : (match && now.Second == _second);
			match = (_millisecond == null) ? match : (match && now.Millisecond == _millisecond);
			return match;
		}

		protected override Metadata? RaiseEvent(Metadata? __metadata = null)
		{
			Notifier?.Invoke();
			return new Metadata(this,
				["Событие в BaseAlarm"],
				MetadataStatus.Success);
		}

		protected override async Task RunAsync(CancellationToken cancellationToken)
		{
			while(!cancellationToken.IsCancellationRequested)
			{
				try
				{
					bool matched = IsTimeCatched();

					if(matched)
					{
						if(!_hasFired)
						{
							RaiseEvent();
							_hasFired = true;
						}
					}
					else
					{
						_hasFired = false;
					}

					await Task.Delay(200, cancellationToken);
				}
				catch(TaskCanceledException)
				{
					break;
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex);
					break;
				}
			}
		}

		#endregion Protected Methods
	}
}