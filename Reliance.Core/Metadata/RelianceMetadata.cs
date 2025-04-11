namespace Reliance.Core.Metadata
{
    /// <summary>
    /// Defines the RelianceMetadataStatus
    /// </summary>
    public enum RelianceMetadataStatus
    {
        /// <summary>
        /// Defines the Error
        /// </summary>
        Error,

        /// <summary>
        /// Defines the Success
        /// </summary>
        Success,

        /// <summary>
        /// Defines the Warning
        /// </summary>
        Warning,

        /// <summary>
        /// Defines the Information
        /// </summary>
        Information,

        /// <summary>
        /// Defines the Unidentified
        /// </summary>
        Unidentified
    }

    public record RelianceMetadata
    {
        public readonly string Name;
        public readonly DateTime DateTime;
        public readonly string[] Args;
        public readonly object Caller;
        public readonly RelianceMetadataStatus Status;

        public RelianceMetadata(object caller, string[] args, RelianceMetadataStatus status)
        {
            Caller = caller;
            Args = args;
            Name = caller.GetType().Name;
            DateTime = DateTime.Now;
            Status = status;
        }
    }
}
