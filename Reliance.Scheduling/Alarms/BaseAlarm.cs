namespace Reliance.Scheduling.Alarms
{
	using Reliance.Utility.Metadata;
	using System;
	using System.Threading.Tasks;

	/// <summary>
	/// Defines the <see cref="BaseAlarm" />
	/// </summary>
	public abstract class BaseAlarm : IAlarm, IAlarmInnards, IDisposable
	{
		/// <summary>
		/// Defines the _cancellationTokenSource
		/// </summary>
		private CancellationTokenSource? _cancellationTokenSource = new();

		/// <summary>
		/// Defines the Notifier
		/// </summary>
		public event IAlarm.IAlarmHandler? Notifier;

		/// <summary>
		/// Gets the InternalToken
		/// </summary>
		protected CancellationToken? InternalToken => _cancellationTokenSource?.Token;

		/// <summary>
		/// Finalizer <see cref="BaseAlarm"/>
		/// </summary>
		~BaseAlarm() => Dispose();

		/// <summary>
		/// The Dispose
		/// </summary>
		public void Dispose()
		{
			if(_cancellationTokenSource != null)
			{
				_cancellationTokenSource.Cancel();
				_cancellationTokenSource.Dispose();
				_cancellationTokenSource = null;
			}
		}
		/// <summary>
		/// The InternalRunAsync
		/// </summary>
		/// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
		/// <returns>The <see cref="Task"/></returns>

		/// <summary>
		/// The InternalRaiseEvent
		/// </summary>
		/// <returns>The <see cref="RelianceMetadata?"/></returns>
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
