using Reliance.Utility.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reliance.Core.Config;

namespace Reliance.Logging.Message
{
    public class TimeMessageLineBuilder : IMessageLineBuilder
    {
        private static readonly string _timeFormat = RegisterTimeFormat();
        public string Build(Metadata? __metadata = null)
        {
            string timeFormat = (__metadata == null) ? _timeFormat : __metadata.Args[0];
            return DateTime.Now.ToString(timeFormat);
        }

        private static string RegisterTimeFormat()
        {
            Guid partId = GlobalConfigStorage.Instance["loggerConfiguration"].First();
            Guid lineId = GlobalConfigStorage.Instance[partId]["timeFormat"].First();
            string? timeFormat = GlobalConfigStorage.Instance[partId][lineId].Value.ToString();

            return timeFormat ?? "hh:mm:ss:fff";
        }
    }
}
