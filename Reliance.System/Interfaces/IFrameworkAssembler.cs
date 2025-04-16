namespace Reliance.System.Interfaces
{
	using Reliance.Utility.Metadata;

	public interface IFrameworkAssembler
	{
		public Metadata? BuildCoreModule();

		public Metadata? BuildApiModule();

		public Metadata? BuildLoggingModule();

		public Metadata? BuildSchedulingModule();

		public Metadata? BuildCachingModule();

		public Metadata? BuildAnalyzeModule();

		public Metadata? BuildPluginModule();
	}
}
