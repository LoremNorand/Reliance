namespace Reliance.Utility.Metadata
{
	public static class MetadataMediator
	{
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

	}
}
