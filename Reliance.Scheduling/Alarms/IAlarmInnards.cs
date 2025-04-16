using Reliance.Utility.Metadata;

namespace Reliance.Scheduling.Alarms
{
	internal interface IAlarmInnards
	{
		protected Metadata? InternalRegister(Metadata? __metadata = null);
		protected Task InternalRunAsync(CancellationToken cancellationToken);
		protected Metadata? InternalRaiseEvent(Metadata? __metadata = null);
	}
}
