using Reliance.Utility.Metadata;

namespace Reliance.Logging.Message
{
    public interface IMessageLineBuilder
    {
        public string Build(Metadata? __metadata = null);
    }
}
