using System;
using cyrka.api.domain.events;

namespace cyrka.api.domain.customers.commands.register
{
	public class CustomerRegistered : EventData
	{
		public readonly string Id;
		public readonly string Name;
		public readonly string Description;

		public CustomerRegistered(string id, string name, string description)
		{
			Id = id;
			Name = name;
			Description = description;
		}
	}
}
