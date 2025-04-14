namespace Reliance.System.Extensions
{
	using Microsoft.Extensions.DependencyInjection;
	using Reliance.System.Interfaces;

	/// <summary>
	/// Defines the <see cref="RelianceModuleExtensions" />
	/// </summary>
	public static class RelianceModuleExtensions
	{
		/// <summary>
		/// The AddRelianceModules
		/// </summary>
		/// <param name="services">The services<see cref="IServiceCollection"/></param>
		/// <returns>The <see cref="IServiceCollection"/></returns>
		public static IServiceCollection AddRelianceModules(this IServiceCollection services)
		{
			services.AddTransient<IFrameworkAssembler, IFrameworkAssembler>();

			services.AddHostedService<Builder>();
			return services;
		}
	}
}
