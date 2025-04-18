namespace Reliance.Scheduling.Alarms
{
	using Reliance.Utility.Identity;
	using Reliance.Utility.Metadata;
	using System;
	using System.Threading.Tasks;

	public abstract class BaseAlarm : IAlarm, IAlarmInnards, IDisposable
	{
		protected CancellationTokenSource? _cancellationTokenSource = new();
		protected string _name;

		protected BaseAlarm(string name = "")
		{
			if((name == null) || (name.Length == 0))
				name = "Undefined-" + ObjectIdGenerator.Next();
			Name = name;
			Register();
		}

		public string Name
		{
			get => _name;
			set
			{
				string buffer = value;
				if(AlarmVault.Instance.IsOccupied(buffer))
				{
					buffer = $"{buffer}-{ObjectIdGenerator.Next()}";
					MetadataMediator.ValueWasChanged(value, buffer, this);
				}
				_name = buffer;
			}
		}

		public abstract event IAlarm.IAlarmHandler? Notifier;

		protected CancellationToken? InternalToken => _cancellationTokenSource?.Token;

		~BaseAlarm() => Dispose();

		public void Dispose()
		{
			if(_cancellationTokenSource != null)
			{
				_cancellationTokenSource.Cancel();
				_cancellationTokenSource.Dispose();
				_cancellationTokenSource = null;
			}
		}

		public virtual void Start()
		{
			_cancellationTokenSource = new CancellationTokenSource();
			Task.Run(() => RunAsync(_cancellationTokenSource.Token));
		}

		public virtual void Start(TimeSpan delay)
		{

		}

		Metadata? IAlarmInnards.InternalRaiseEvent(Metadata? __metadata)
			=> RaiseEvent(__metadata);

		Metadata? IAlarmInnards.InternalRegister(Metadata? __metadata)
			=> Register(__metadata);

		Task IAlarmInnards.InternalRunAsync(CancellationToken cancellationToken)
			=> RunAsync(CancellationTokenSource.
				CreateLinkedTokenSource(cancellationToken, _cancellationTokenSource.Token).Token);

		protected abstract Metadata? RaiseEvent(Metadata? __metadata = null);
		private Metadata? Register(Metadata? __metadata = null)
		{
			AlarmVault.Instance[_name] = this;
			return new Metadata(this, ["BaseAlarm Register()"], MetadataStatus.Success);
		}
		protected abstract Task RunAsync(CancellationToken cancellationToken);
	}
}
