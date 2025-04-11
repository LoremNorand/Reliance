namespace Reliance.System.Interfaces
{
    using Reliance.Core.Metadata;

    /// <summary>
    /// Defines the <see cref="IDependencyBuilder" />
    /// </summary>
    public interface IDependencyBuilder
    {
        /// <summary>
        /// The BuildCoreModule
        /// </summary>
        /// <returns>The <see cref="RelianceMetadata"/></returns>
        public RelianceMetadata? BuildCoreModule();

        /// <summary>
        /// The BuildLoggingModule
        /// </summary>
        /// <returns>The <see cref="RelianceMetadata"/></returns>
        public RelianceMetadata? BuildLoggingModule();

        /// <summary>
        /// The BuildPluginModule
        /// </summary>
        /// <returns>The <see cref="RelianceMetadata"/></returns>
        public RelianceMetadata? BuildPluginModule();

        /// <summary>
        /// The BuildSchedulingModule
        /// </summary>
        /// <returns>The <see cref="RelianceMetadata"/></returns>
        public RelianceMetadata? BuildSchedulingModule();

        /// <summary>
        /// The BuildCachingModule
        /// </summary>
        /// <returns>The <see cref="RelianceMetadata"/></returns>
        public RelianceMetadata? BuildCachingModule();

        /// <summary>
        /// The BuildApiModule
        /// </summary>
        /// <returns>The <see cref="RelianceMetadata"/></returns>
        public RelianceMetadata? BuildApiModule();
    }
}
