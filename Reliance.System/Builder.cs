namespace Reliance.System
{
    using Reliance.Core.Metadata;
    using Reliance.System.Implementations;
    using Reliance.System.Interfaces;

    /// <summary>
    /// Defines the <see cref="Builder" />
    /// </summary>
    public class Builder
    {
        /// <summary>
        /// Defines the _instance
        /// </summary>
        private Builder _instance = new Builder();

        /// <summary>
        /// Gets the Instance
        /// </summary>
        public Builder Instance { get => _instance ?? (_instance = new Builder()); }

        /// <summary>
        /// Prevents a default instance of the <see cref="Builder"/> class from being created.
        /// </summary>
        private Builder() {}

        /// <summary>
        /// The Build
        /// </summary>
        /// <param name="Args">The Args<see cref="string[]"/></param>
        /// <param name="dependencyBuilder">The dependencyBuilder<see cref="IDependencyBuilder"/></param>
        /// <returns>The <see cref="RelianceMetadata?"/></returns>
        public RelianceMetadata? Build(string[] args, IDependencyBuilder? dependencyBuilder)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<Worker>();

            BuildDependencies(dependencyBuilder);

            var host = builder.Build();
            host.Run();

            string[] __result_args = { "Reliance.System", "Build()" };
            return new RelianceMetadata(this, __result_args, RelianceMetadataStatus.Success);
        }

        private static void BuildDependencies(IDependencyBuilder? dependencyBuilder)
        {
            var services = new ServiceCollection().
                AddTransient<IDependencyBuilder, DependencyBuilder>();

            using var serviceProvider = services.BuildServiceProvider();
            
            if(dependencyBuilder == null)
                dependencyBuilder = serviceProvider.GetService<IDependencyBuilder>();

            if (dependencyBuilder == null)
            {
                string stack = Environment.StackTrace;
                throw new InvalidOperationException($"""
                    {DateTime.Now} | Ошибка сборки Reliance.
                    -----
                    Stack: {stack}
                    -----
                    Реализация IDependencyBuilder равна null
                    """);
            }

            dependencyBuilder.BuildCoreModule();
            dependencyBuilder.BuildLoggingModule();
            dependencyBuilder.BuildSchedulingModule();
            dependencyBuilder.BuildCachingModule();
            dependencyBuilder.BuildApiModule();
            dependencyBuilder.BuildPluginModule();
        }
    }
}
