namespace Reliance.Communication.Entry
{
	public interface IEntry
	{
		#region Public Properties

		public object? Value { get; }
		public string Key { get; init; }

		#endregion Public Properties
	}
}