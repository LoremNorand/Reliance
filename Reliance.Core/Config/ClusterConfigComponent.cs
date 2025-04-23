using Reliance.Core.Storage;

namespace Reliance.Core.Config
{
    public class ClusterConfigComponent : BaseStorageComponent, IConfigElement
    {
        private readonly string _name;
        private readonly string? _group;
        private readonly object _value;

        public override string Name => _name;

        public string Group => _group ?? "";

        public object Value => _value;

        public ClusterConfigComponent(string name, string? group, object value)
        {
            _name = name;
            _group = group;
            _value = value;
        }
    }
}
