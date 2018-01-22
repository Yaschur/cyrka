using System;
using cyrka.api.common.events;

namespace cyrka.api.domain.customers.commands.change
{
	public class CustomerChanged : CustomerEventData
	{
		public readonly string Name;
		public readonly string Description;

		public CustomerChanged(string id, string name, string description)
			: base(id)
		{
			Name = name;
			Description = description;
		}
	}
}
