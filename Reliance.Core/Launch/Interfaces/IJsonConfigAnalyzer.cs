using Reliance.Utility.Metadata;

namespace Reliance.Core.Launch.Interfaces
{
    internal interface IJsonConfigAnalyzer
    {
        public RelianceMetadata? Analyze();
    }
}
