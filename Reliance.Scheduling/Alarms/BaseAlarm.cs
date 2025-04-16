namespace Reliance.Scheduling.Alarms
{
	using Reliance.Utility.Identity;
	using Reliance.Utility.Metadata;
	using System;
	using System.Threading.Tasks;

	public abstract class BaseAlarm : IAlarm, IAlarmInnards, IDisposable
	{
		private CancellationTokenSource _cancellationTokenSource = new();
		protected string _name = "";

		public string Name
		{
			get => _name;
			set
			{
				string buffer = value;
				if(AlarmVault.Instance.IsOccupied(buffer))
				{
					buffer += ObjectIdGenerator.Next();
					var __meta = new Metadata(this,
					[
						"name was occupied",
						$"old = {value}",
						$"new = {buffer}"
					], 
					RelianceMetadataStatus.Warning);
				}
				_name = buffer;
			}
		}

		public event IAlarm.IAlarmHandler? Notifier;

		protected CancellationToken InternalToken => _cancellationTokenSource.Token;

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

		Metadata? IAlarmInnards.InternalRaiseEvent(Metadata? __metadata)
			=> RaiseEvent(__metadata);

		Metadata? IAlarmInnards.InternalRegister(Metadata? __metadata)
			=> Register(__metadata);

		Task IAlarmInnards.InternalRunAsync(CancellationToken cancellationToken)
			=> RunAsync(CancellationTokenSource.
				CreateLinkedTokenSource(cancellationToken, _cancellationTokenSource.Token).Token);

		protected abstract Metadata? RaiseEvent(Metadata? __metadata = null);
		private Metadata? Register(Metadata? __metadata)
		{
			AlarmVault.Instance[_name] = this;
			return new Metadata(this, ["BaseAlarm Register()"], RelianceMetadataStatus.Success);
		}
		protected abstract Task RunAsync(CancellationToken cancellationToken);
	}
}
