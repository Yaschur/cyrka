using System;
using cyrka.api.common.events;

namespace cyrka.api.domain.customers.commands.changeTitle
{
	public class ChangeTitleHandler
	{
		public EventData[] Handle(ChangeTitle command)
		{
			var titleChanged = new TitleChanged(command.CustomerId, command.TitleId, command.Name, command.NumberOfSeries, command.Description);
			return new[] { titleChanged };
		}
	}
}
