using System;
using cyrka.api.domain.events;

namespace cyrka.api.domain.customers.register
{
	public class CustomerRegistered : EventData
	{
		public readonly string Name;
		public readonly string Description;

		public CustomerRegistered(string name, string description)
		{
			Name = name;
			Description = description;
		}
	}
}
