namespace Reliance.Utility.Metadata
{
	public enum MetadataStatus
	{
		Error,

		Success,

		Warning,

		Information,

		Unidentified
	}

	public record Metadata
	{
		public readonly string Name;
		public readonly DateTime CallTime;
		public readonly string[] Args;
		public readonly object Caller;
		public readonly MetadataStatus Status;

		public Metadata(object caller, string[] args, MetadataStatus status)
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
                Аргументы: {string.Join(",", Args)}\n
                """;
		}
	}
}
