namespace Reliance.Core.Storage
{
	public interface IStorageComponent
	{
		public string Name { get; }
		public Guid Guid { get; }
	}
}
