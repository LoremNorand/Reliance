namespace Reliance.Utility.Metadata
{
	public static class MetadataMediator
	{
		public static void ValueWasChanged<T>(T oldValue, T newValue, object sender)
		{
			string oldFormatted = oldValue.ToString();
			string newFormatted = newValue.ToString();

			Metadata? metadata = new Metadata(sender,
				[
				"Неявное изменение значения.",
				$"Старое: {oldFormat}"
				]);
		}
		
	}
}
