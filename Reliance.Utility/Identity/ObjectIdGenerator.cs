using System.Text;

namespace Reliance.Utility.Identity
{
	public static class ObjectIdGenerator
	{
		private static int _generationsCount = 0;

		public static string Next()
		{
			byte[] bytes = Encoding.ASCII.GetBytes(_generationsCount.ToString());
			_generationsCount++;
			return Convert.ToBase64String(bytes);
		}
	}
}
