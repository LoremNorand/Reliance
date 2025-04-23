using Reliance.Core.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reliance.Core.Config
{
    public class PartialConfigStorage : BaseStorageComponent, IStorage<ClusterConfigComponent>
    {
        #region Private Fields

        private readonly Dictionary<Guid, ClusterConfigComponent> _storage = new();

        #endregion Private Fields



        #region Private Constructors

        private PartialConfigStorage()
        { }

        #endregion Private Constructors



        #region Public Properties

        public Dictionary<Guid, ClusterConfigComponent> Storage
        {
            get => _storage;
        }

        public override string Name => throw new NotImplementedException();

        #endregion Public Properties



        #region Public Indexers

        public List<Guid> this[string name] =>
            _storage.Where(kv => kv.Value.Name == name).
            Select(kv => kv.Key).ToList();

        public ClusterConfigComponent this[Guid guid]
        {
            get => _storage[guid];
        }

        #endregion Public Indexers



        #region Public Methods

        public void Register(ClusterConfigComponent obj)
        {
            if (!_storage.ContainsKey(obj.Guid))
                _storage[obj.Guid] = obj;
        }

        public void Unregister(ClusterConfigComponent obj)
        {
            _storage.Remove(obj.Guid);
        }

        #endregion Public Methods
    }
}
