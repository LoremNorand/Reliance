namespace Reliance.Scheduling.Alarms
{
	using Reliance.Utility.Metadata;
	using System;
	using System.Threading.Tasks;

	public abstract class BaseAlarm : IAlarm, IAlarmInnards, IDisposable
	{
		private CancellationTokenSource? _cancellationTokenSource = new();

		public event IAlarm.IAlarmHandler? Notifier;

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

		RelianceMetadata? IAlarmInnards.InternalRaiseEvent(RelianceMetadata? __metadata)
			=> RaiseEvent(__metadata);

		RelianceMetadata? IAlarmInnards.InternalRegister(RelianceMetadata? __metadata)
			=> Register(__metadata);

		Task IAlarmInnards.InternalRunAsync(CancellationToken cancellationToken)
			=> RunAsync(cancellationToken);

		protected abstract RelianceMetadata? RaiseEvent(RelianceMetadata? __metadata = null);
		private RelianceMetadata? Register(RelianceMetadata? __metadata)
		{
			throw new NotImplementedException();
		}
		protected abstract Task RunAsync(CancellationToken cancellationToken);
	}
}
