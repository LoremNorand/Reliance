namespace Reliance.System
{
	using Microsoft.Extensions.Hosting;
	using Reliance.Utility.Metadata;
	using Reliance.System.Implementations;
	using Reliance.System.Interfaces;

	public class Builder : IHostedService
	{
		private readonly IFrameworkAssembler _frameworkAssembler = new FrameworkAssembler();

		public Builder(IFrameworkAssembler? frameworkAssembler)
		{
			if(frameworkAssembler != null)
			{
				_frameworkAssembler = frameworkAssembler;
			}
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			List<Metadata?> executions =
			[
				_frameworkAssembler.BuildCoreModule(),
				_frameworkAssembler.BuildPluginModule(),
				_frameworkAssembler.BuildSchedulingModule(),
				_frameworkAssembler.BuildLoggingModule(),
				_frameworkAssembler.BuildAnalyzeModule(),
				_frameworkAssembler.BuildApiModule(),
			];

			if(executions.Any((x) => (x != null) && (x.Status == MetadataStatus.Error)))
			{
				// TODO: Логгирование об ошибке
			}
			return Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
