using System.Threading.Tasks;
using cyrka.api.common.commands;
using cyrka.api.common.events;

namespace cyrka.api.domain.projects.commands.setProduct
{
	public class SetProductHandler : IAggregateCommandHandler<SetProduct, ProjectAggregate>
	{
		public Task<EventData[]> Handle(SetProduct command, ProjectAggregate aggregate)
		{
			var eventData = new ProductSet(
				aggregate.State.ProjectId,
				command.CustomerId,
				command.CustomerName,
				command.TitleId,
				command.TitleName,
				command.TotalEpisodes,
				command.EpisodeNumber,
				command.EpisodeDuration
			);

			return Task.FromResult<EventData[]>(new[] { eventData });
		}
	}
}
