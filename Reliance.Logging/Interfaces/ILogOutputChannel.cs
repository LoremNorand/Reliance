namespace Reliance.Logging.Interfaces
{
	public interface ILogOutputChannel<T>
	{
		public void Post(T message);
	}
}
