namespace Reliance.System.Interfaces
{
	using Reliance.Utility.Metadata;

	/// <summary>
	/// Defines the <see cref="IFrameworkAssembler" />
	/// </summary>
	public interface IFrameworkAssembler
	{
		/// <summary>
		/// The BuildCoreModule
		/// </summary>
		/// <returns>The <see cref="RelianceMetadata?"/></returns>
		public RelianceMetadata? BuildCoreModule();

		/// <summary>
		/// The BuildApiModule
		/// </summary>
		/// <returns>The <see cref="RelianceMetadata?"/></returns>
		public RelianceMetadata? BuildApiModule();

		/// <summary>
		/// The BuildLoggingModule
		/// </summary>
		/// <returns>The <see cref="RelianceMetadata?"/></returns>
		public RelianceMetadata? BuildLoggingModule();

		/// <summary>
		/// The BuildSchedulingModule
		/// </summary>
		/// <returns>The <see cref="RelianceMetadata?"/></returns>
		public RelianceMetadata? BuildSchedulingModule();

		/// <summary>
		/// The BuildCachingModule
		/// </summary>
		/// <returns>The <see cref="RelianceMetadata?"/></returns>
		public RelianceMetadata? BuildCachingModule();

		/// <summary>
		/// The BuildAnalyzeModule
		/// </summary>
		/// <returns>The <see cref="RelianceMetadata?"/></returns>
		public RelianceMetadata? BuildAnalyzeModule();

		/// <summary>
		/// The BuildPluginModule
		/// </summary>
		/// <returns>The <see cref="RelianceMetadata?"/></returns>
		public RelianceMetadata? BuildPluginModule();
	}
}
