namespace Reliance.System
{
	using Microsoft.Extensions.Hosting;
	using Reliance.Core.Metadata;
	using Reliance.System.Implementations;
	using Reliance.System.Interfaces;

	/// <summary>
	/// Defines the <see cref="Builder" />
	/// </summary>
	public class Builder : IHostedService
	{
		/// <summary>
		/// Defines the _frameworkAssembler
		/// </summary>
		private readonly IFrameworkAssembler _frameworkAssembler = new FrameworkAssembler();

		/// <summary>
		/// Initializes a new instance of the <see cref="Builder"/> class.
		/// </summary>
		/// <param name="frameworkAssembler">The frameworkAssembler<see cref="IFrameworkAssembler?"/></param>
		public Builder(IFrameworkAssembler? frameworkAssembler)
		{
			if(frameworkAssembler != null)
			{
				_frameworkAssembler = frameworkAssembler;
			}
		}

		/// <summary>
		/// The StartAsync
		/// </summary>
		/// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
		/// <returns>The <see cref="Task"/></returns>
		public Task StartAsync(CancellationToken cancellationToken)
		{
			List<RelianceMetadata?> executions =
			[
				_frameworkAssembler.BuildCorePacket(),
				_frameworkAssembler.BuildPluginPacket(),
				_frameworkAssembler.BuildSchedulingPacket(),
				_frameworkAssembler.BuildLoggingPacket(),
				_frameworkAssembler.BuildAnalyzePacket(),
				_frameworkAssembler.BuildApiPacket(),
			];

			if(executions.Any((x) => (x != null) && (x.Status == RelianceMetadataStatus.Error)))
			{
				// TODO: Логгирование об ошибке
			}
			return Task.CompletedTask;
		}

		/// <summary>
		/// The StopAsync
		/// </summary>
		/// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
		/// <returns>The <see cref="Task"/></returns>
		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
