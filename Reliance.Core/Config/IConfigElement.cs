namespace Reliance.Core.Config
{
    public interface IConfigElement
    {
        public string Group { get; }
        public string Name { get; }
        public object Value { get; }
    }
}
