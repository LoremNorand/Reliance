using System.Text;

namespace Reliance.Utility.Identity
{
	public static class ObjectIdGenerator
	{
		#region Private Fields

		private static int _generationsCount = 0;

		#endregion Private Fields



		#region Public Methods

		public static string Next()
		{
			byte[] bytes = Encoding.ASCII.GetBytes(_generationsCount.ToString());
			_generationsCount++;
			return Convert.ToBase64String(bytes);
		}

		#endregion Public Methods
	}
}