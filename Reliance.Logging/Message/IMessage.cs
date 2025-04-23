namespace Reliance.Logging.Message
{
    public interface IMessage
    {
        public Dictionary<string, string> PublicLines { get; }
        public Dictionary<string, string> InternalLines { get; }
    }
}
