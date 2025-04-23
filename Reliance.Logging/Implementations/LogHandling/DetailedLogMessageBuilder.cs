namespace Reliance.Logging.Implementations.LogHandling
{
	using Reliance.Logging.Implementations.LogMessage;
	using Reliance.Logging.Interfaces;
	using Reliance.Utility.Identity;
	using Reliance.Utility.Metadata;
	using System.Collections.Generic;

	public class DetailedLogMessageBuilder : ILogMessageBuilder
	{
		private Dictionary<string, string> _components = new();
		public ILogMessageBuilder Add((string key, string value) keyValuePair)
		{
			UpdateComponents(new KeyValuePair<string, string>(
					keyValuePair.key,
					keyValuePair.value));
			return this;
		}

		public ILogMessageBuilder Add(KeyValuePair<string, string> keyValuePair)
		{
			UpdateComponents(keyValuePair);
			return this;
		}

		public ILogMessage Build()
		{
			return new DetaultLogMessage(_components);
		}

		private void UpdateComponents(KeyValuePair<string, string> keyValuePair)
		{
			if(_components.ContainsKey(keyValuePair.Key))
			{
				string newKey = $"{keyValuePair.Key}-{ObjectIdGenerator.Next()}";
				MetadataMediator.ValueWasChanged(keyValuePair.Key, newKey, this);
				_components[newKey] = keyValuePair.Value;
			}
			_components[keyValuePair.Key] = keyValuePair.Value;
		}
	}
}
