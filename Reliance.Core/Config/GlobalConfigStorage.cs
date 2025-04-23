using Reliance.Core.Storage;

namespace Reliance.Core.Config
{
    public class GlobalConfigStorage : IStorage<PartialConfigStorage>
    {
        #region Private Fields

        private static GlobalConfigStorage _instance = new();
        private readonly Dictionary<Guid, PartialConfigStorage> _storage = new();

        #endregion Private Fields



        #region Private Constructors

        private GlobalConfigStorage()
        { }

        #endregion Private Constructors



        #region Public Properties

        public static GlobalConfigStorage Instance
        {
            get => _instance ?? new();
        }

        public Dictionary<Guid, PartialConfigStorage> Storage
        {
            get => _storage;
        }

        #endregion Public Properties



        #region Public Indexers

        public List<Guid> this[string name] =>
            _storage.Where(kv => kv.Value.Name == name).
            Select(kv => kv.Key).ToList();

        public PartialConfigStorage this[Guid guid]
        {
            get => _storage[guid];
        }

        #endregion Public Indexers



        #region Public Methods

        public void Register(PartialConfigStorage obj)
        {
            if (!_storage.ContainsKey(obj.Guid))
                _storage[obj.Guid] = obj;
        }

        public void Unregister(PartialConfigStorage obj)
        {
            _storage.Remove(obj.Guid);
        }

        #endregion Public Methods
    }
}
