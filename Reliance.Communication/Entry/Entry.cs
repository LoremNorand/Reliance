namespace Reliance.Communication.Entry
{
	public class Entry : IEntry
	{
		protected string _key;
		protected object? _value;

		public string Key 
		{
			get => _key;
			init => _key = value;
		}

		public object? Value
		{
			get => _value;
			init => _value = value;
		}
	}
}
