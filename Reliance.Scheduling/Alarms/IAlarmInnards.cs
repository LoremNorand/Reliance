using Reliance.Utility.Metadata;

namespace Reliance.Scheduling.Alarms
{
	internal interface IAlarmInnards
	{
		protected RelianceMetadata? InternalRegister(RelianceMetadata? __metadata = null);
		protected Task InternalRunAsync(CancellationToken cancellationToken);
		protected RelianceMetadata? InternalRaiseEvent(RelianceMetadata? __metadata = null);
	}
}
