namespace Reliance.Logging.Interfaces
{
	public interface ILogOutputChannel
	{
		public void Post<T>(T message);
	}
}
