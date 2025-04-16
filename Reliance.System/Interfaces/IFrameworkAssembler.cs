namespace Reliance.System.Interfaces
{
	using Reliance.Utility.Metadata;

	public interface IFrameworkAssembler
	{
		public RelianceMetadata? BuildCoreModule();

		public RelianceMetadata? BuildApiModule();

		public RelianceMetadata? BuildLoggingModule();

		public RelianceMetadata? BuildSchedulingModule();

		public RelianceMetadata? BuildCachingModule();

		public RelianceMetadata? BuildAnalyzeModule();

		public RelianceMetadata? BuildPluginModule();
	}
}
