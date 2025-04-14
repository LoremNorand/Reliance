using Reliance.Utility.Metadata;

namespace Reliance.Core.DI
{
	public interface ICoreModuleService
	{
		public RelianceMetadata? BuildCoreModule();
	}
}
