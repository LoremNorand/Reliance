namespace Reliance.System
{
    /// <summary>
    /// Defines the <see cref="Worker" />
    /// </summary>
    public class Worker : BackgroundService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Worker"/> class.
        /// </summary>
        public Worker() { }

        /// <summary>
        /// The ExecuteAsync
        /// </summary>
        /// <param name="stoppingToken">The stoppingToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task"/></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
