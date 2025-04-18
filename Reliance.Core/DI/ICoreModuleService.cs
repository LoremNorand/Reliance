using Reliance.Utility.Metadata;

namespace Reliance.Core.DI
{
	public interface ICoreModuleService
	{
		#region Public Methods

		public Metadata? BuildCoreModule();

		#endregion Public Methods
	}
}