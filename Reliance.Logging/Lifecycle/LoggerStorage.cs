using Reliance.Core.Storage;
using Reliance.Logging.Implementations;

namespace Reliance.Logging.Lifecycle
{
	public class LoggerStorage : IStorage<LoggerNode>
	{
		#region Private Fields

		private static LoggerStorage _instance = new();
		private readonly Dictionary<Guid, LoggerNode> _storage = new();

		#endregion Private Fields



		#region Private Constructors

		private LoggerStorage()
		{ }

		#endregion Private Constructors



		#region Public Properties

		public static LoggerStorage Instance
		{
			get => _instance ?? new();
		}

		public Dictionary<Guid, LoggerNode> Storage
		{
			get => _storage;
		}

		#endregion Public Properties



		#region Public Indexers

		public List<Guid> this[string name] =>
			_storage.Where(kv => kv.Value.Name == name).
			Select(kv => kv.Key).ToList();

		public LoggerNode this[Guid guid]
		{
			get => _storage[guid];
		}

		#endregion Public Indexers



		#region Public Methods

		public void Register(LoggerNode obj)
		{
			if(!_storage.ContainsKey(obj.Guid))
				_storage[obj.Guid] = obj;
		}

		public void Unregister(LoggerNode obj)
		{
			_storage.Remove(obj.Guid);
		}

		#endregion Public Methods
	}
}