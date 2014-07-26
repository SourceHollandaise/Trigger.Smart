using System;

namespace Trigger.XStorable.DataStore
{
	public class GuidIdGenerator : IdGenerator
	{
		public object GetId()
		{
			return Guid.NewGuid().ToString().Replace("-", "");
		}
	}
}
