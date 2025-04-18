namespace Reliance.Utility.Metadata
{
	public static class MetadataMediator
	{
		#region Public Methods

		public static void ValueWasChanged<T>(T oldValue, T newValue, object sender)
		{
			Metadata? metadata = new Metadata(sender,
				[
				"Неявное изменение значения.",
				$"Старое: {oldValue}",
				$"Новое: {newValue}"
				],
				MetadataStatus.Warning);
		}

		#endregion Public Methods
	}
}