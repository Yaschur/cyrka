using System;
using cyrka.api.common.events;

namespace cyrka.api.domain.customers.commands.removeTitle
{
	public class RemoveTitleHandler
	{
		public EventData[] Handle(RemoveTitle command)
		{
			var titleRemoved = new TitleRemoved(command.CustomerId, command.TitleId);
			return new[] { titleRemoved };
		}
	}
}
