using Microsoft.Extensions.DependencyInjection;

namespace Reliance.Core.DI
{
	public static class CoreModuleExtensions
	{
		#region Public Methods

		public static IServiceCollection AddCoreModules(this IServiceCollection service)
		{
			return service;
		}

		#endregion Public Methods
	}
}