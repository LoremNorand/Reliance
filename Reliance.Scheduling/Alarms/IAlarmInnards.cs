using Reliance.Utility.Metadata;

namespace Reliance.Scheduling.Alarms
{
	internal interface IAlarmInnards
	{
		#region Protected Methods

		protected Metadata? InternalRaiseEvent(Metadata? __metadata = null);

		protected Metadata? InternalRegister(Metadata? __metadata = null);

		protected Task InternalRunAsync(CancellationToken cancellationToken);

		#endregion Protected Methods
	}
}