using Reliance.Communication.Entry;

namespace Reliance.Communication.Metadata
{
	public class EntryMetadata : Metadata
	{
		protected readonly object? _sender;
		protected int _priority;
		public override object? Sender 
		{ 
			get => _sender; 
			init => _sender = value; 
		}

	}
}
