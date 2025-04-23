using System.Xml;

namespace Reliance.Core.Storage
{
	public interface IStorage<TComponent> where TComponent : BaseStorageComponent
	{
		public List<Guid> this[string name] { get; }
		public TComponent this[Guid guid] { get; }
		public Dictionary<Guid, TComponent> Storage { get; }

		public void Register(TComponent obj);
		public void Unregister(TComponent obj);
	}
}
