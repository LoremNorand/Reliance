namespace Reliance.System.Extensions
{
	using Microsoft.Extensions.DependencyInjection;
	using Reliance.System.Interfaces;

	public static class RelianceModuleExtensions
	{
		public static IServiceCollection AddRelianceModules(this IServiceCollection services)
		{
			services.AddTransient<IFrameworkAssembler, IFrameworkAssembler>();

			services.AddHostedService<Builder>();
			return services;
		}
	}
}
