using System;
using cyrka.api.common.events;

namespace cyrka.api.domain.customers.commands.registerTitle
{
	public class RegisterTitleHandler
	{
		public EventData[] Handle(RegisterTitle command)
		{
			var id = Guid.NewGuid().ToString();
			var titleRegistered = new TitleRegistered(command.CustomerId, id, command.Name, command.NumberOfSeries, command.Description);
			return new[] { titleRegistered };
		}
	}
}
