namespace Reliance.Utility.Metadata
{
	public record Metadata
	{
		#region Public Fields

		public readonly string[] Args;
		public readonly object Caller;
		public readonly DateTime CallTime;
		public readonly string Name;
		public readonly MetadataStatus Status;

		#endregion Public Fields



		#region Public Constructors

		public Metadata(object caller, string[] args, MetadataStatus status)
		{
			Caller = caller;
			Args = args;
			Name = caller.GetType().Name;
			CallTime = DateTime.Now;
			Status = status;
		}

		#endregion Public Constructors



		#region Public Methods

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

		#endregion Public Methods
	}
}