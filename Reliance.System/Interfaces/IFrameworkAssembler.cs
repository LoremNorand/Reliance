namespace Reliance.System.Interfaces
{
	using Reliance.Core.Metadata;

	/// <summary>
	/// Defines the <see cref="IFrameworkAssembler" />
	/// </summary>
	public interface IFrameworkAssembler
	{
		/// <summary>
		/// The BuildCorePacket
		/// </summary>
		/// <returns>The <see cref="RelianceMetadata?"/></returns>
		public RelianceMetadata? BuildCorePacket();

		/// <summary>
		/// The BuildApiPacket
		/// </summary>
		/// <returns>The <see cref="RelianceMetadata?"/></returns>
		public RelianceMetadata? BuildApiPacket();

		/// <summary>
		/// The BuildLoggingPacket
		/// </summary>
		/// <returns>The <see cref="RelianceMetadata?"/></returns>
		public RelianceMetadata? BuildLoggingPacket();

		/// <summary>
		/// The BuildSchedulingPacket
		/// </summary>
		/// <returns>The <see cref="RelianceMetadata?"/></returns>
		public RelianceMetadata? BuildSchedulingPacket();

		/// <summary>
		/// The BuildCachingPacket
		/// </summary>
		/// <returns>The <see cref="RelianceMetadata?"/></returns>
		public RelianceMetadata? BuildCachingPacket();

		/// <summary>
		/// The BuildAnalyzePacket
		/// </summary>
		/// <returns>The <see cref="RelianceMetadata?"/></returns>
		public RelianceMetadata? BuildAnalyzePacket();
	}
}
