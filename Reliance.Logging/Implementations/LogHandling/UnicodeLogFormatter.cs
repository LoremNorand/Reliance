using Reliance.Logging.Interfaces;
using System.Text;

namespace Reliance.Logging.Implementations.LogHandling
{
	public class UnicodeLogFormatter : ILogFormatter
	{
		public string Format(ILogMessage message)
		{
			Dictionary<string, string> components = message.Components;
			StringBuilder stringBuilder = new StringBuilder();

			int indent = 0;
			bool isIndentParsed = false;
			if(components.ContainsKey("__Level"))
			{
				isIndentParsed = int.TryParse(components["__Level"], out indent);
				components.Remove("__Level");
			}
			indent = (isIndentParsed) ? 4 * indent : 0;

			string strIndent = "".PadLeft(indent);
			foreach(KeyValuePair<string, string> component in components)
			{
				stringBuilder.AppendLine($"{strIndent}{component.Key}: {component.Value}");
			}

			return stringBuilder.ToString();
		}
	}
}
