namespace Reliance.Utility.Metadata
{
	public enum RelianceMetadataStatus
	{
		Error,

		Success,

		Warning,

		Information,

		Unidentified
	}

	public record RelianceMetadata
	{
		public readonly string Name;
		public readonly DateTime CallTime;
		public readonly string[] Args;
		public readonly object Caller;
		public readonly RelianceMetadataStatus Status;

		public RelianceMetadata(object caller, string[] args, RelianceMetadataStatus status)
		{
			Caller = caller;
			Args = args;
			Name = caller.GetType().Name;
			CallTime = DateTime.Now;
			Status = status;
		}

		public override string? ToString()
		{
			return $"""
                Имя: {Name}
                {CallTime}
                Статус: {Status}
                Объект вызова: {Caller}
                Аргументы: {string.Join(",", Args)}
                """;
		}
	}
}
