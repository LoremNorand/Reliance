using Microsoft.Extensions.DependencyInjection;
using Reliance.Utility.Metadata;

namespace Reliance.Core.DI
{
    public static class CoreModuleExtensions
    {
        public static IServiceCollection AddCoreModules(this IServiceCollection service)
        {
            return service;
        }
    }
}
