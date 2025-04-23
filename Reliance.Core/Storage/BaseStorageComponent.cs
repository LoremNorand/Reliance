using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reliance.Core.Storage
{
	public abstract class BaseStorageComponent : IStorageComponent
	{
		private Guid _guid;
		public abstract string Name { get; }

		protected BaseStorageComponent()
		{
			_guid = Guid.NewGuid();
		}

		public Guid Guid => _guid;
	}
}
