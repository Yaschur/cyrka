using System.Linq;
using System.Threading.Tasks;
using cyrka.api.common.events;

namespace cyrka.api.domain.projects
{
	public class ProjectAggregateRepository
	{
		public ProjectAggregateRepository(IEventStore eventStore)
		{
			_eventStore = eventStore;
		}

		public async Task<ProjectAggregate> GetById(string projectId)
		{
			var aggEventDatas = (await _eventStore
				.FindAllOfAggregateById<ProjectAggregate>(projectId))
				.Select(e => e.EventData)
				.ToArray();

			if (aggEventDatas.Length == 0)
				return null;

			var project = new ProjectAggregate();
			project.ApplyEvents(aggEventDatas);

			return project;
		}

		private readonly IEventStore _eventStore;
	}
}
