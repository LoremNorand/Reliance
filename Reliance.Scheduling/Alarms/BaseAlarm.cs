namespace Reliance.Scheduling.Alarms
{
	using Reliance.Utility.Identity;
	using Reliance.Utility.Metadata;
	using System;
	using System.Threading.Tasks;

	public abstract class BaseAlarm : IAlarm, IAlarmInnards, IDisposable
	{
		#region Protected Fields

		protected CancellationTokenSource? _cancellationTokenSource = new();
		protected string _name;

		#endregion Protected Fields



		#region Protected Constructors

		protected BaseAlarm(string name = "")
		{
			if((name == null) || (name.Length == 0))
				name = "Undefined-" + ObjectIdGenerator.Next();
			Name = name;
			Register();
		}

		#endregion Protected Constructors



 		#region Private Destructors

		~BaseAlarm() => Dispose();

		#endregion Private Destructors



		#region Public Events

		public abstract event IAlarm.IAlarmHandler? Notifier;

		#endregion Public Events



		#region Public Properties

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

		#endregion Public Properties



		#region Protected Properties

		protected CancellationToken? InternalToken => _cancellationTokenSource?.Token;

		#endregion Protected Properties



		#region Public Methods

		public void Dispose()
		{
			if(_cancellationTokenSource != null)
			{
				_cancellationTokenSource.Cancel();
				_cancellationTokenSource.Dispose();
				_cancellationTokenSource = null;
			}
		}

		Metadata? IAlarmInnards.InternalRaiseEvent(Metadata? __metadata)
			=> RaiseEvent(__metadata);

		Metadata? IAlarmInnards.InternalRegister(Metadata? __metadata)
			=> Register(__metadata);

		Task IAlarmInnards.InternalRunAsync(CancellationToken cancellationToken)
			=> RunAsync(CancellationTokenSource.
				CreateLinkedTokenSource(cancellationToken, _cancellationTokenSource.Token).Token);

		public virtual void Start()
		{
			_cancellationTokenSource = new CancellationTokenSource();
			Task.Run(() => RunAsync(_cancellationTokenSource.Token));
		}

		public virtual void Start(TimeSpan delay)
		{
		}

		#endregion Public Methods



		#region Protected Methods

		protected abstract Metadata? RaiseEvent(Metadata? __metadata = null);

		protected abstract Task RunAsync(CancellationToken cancellationToken);

		#endregion Protected Methods

		#region Private Methods

		private Metadata? Register(Metadata? __metadata = null)
		{
			AlarmVault.Instance[_name] = this;
			return new Metadata(this, ["BaseAlarm Register()"], MetadataStatus.Success);
		}

		#endregion Private Methods
	}
}