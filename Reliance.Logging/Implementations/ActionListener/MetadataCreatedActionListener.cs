namespace Reliance.Logging.Implementations.ActionListener
{
	using Reliance.Core.Interfaces;
	using Reliance.Logging.Implementations.LogHandling;
	using Reliance.Utility.Metadata;

	internal class MetadataCreatedActionListener : IActionListener<Metadata>
	{
		#region Public Methods

		public void OnAction(Metadata metadata)
		{
			DetailedLogMessageBuilder messageBuilder = new();
			Interfaces.ILogMessage message = messageBuilder.
				Add(("Имя", "__Metadata_listening")).
				Add(("Время", DateTime.Now.ToString("hh:mm:ss:fff"))).
				Add(("Статус", metadata.Status.ToString())).
				Add(("Сообщение", "Метадата")).
				Build();

			LoggingManager.Instance["__Metadata_listening"]?.Log(message);
		}

		#endregion Public Methods
	}
}