using System.Linq;
using System.Threading.Tasks;
using cyrka.api.common.events;

namespace cyrka.api.domain.jobs
{
	public class JobTypeAggregateRepository
	{
		public JobTypeAggregateRepository(IEventStore eventStore)
		{
			_eventStore = eventStore;
		}

		public async Task<JobTypeAggregate> GetById(string jobTypeId)
		{
			var aggEventDatas = (await _eventStore
				.FindAllOfAggregateById<JobTypeAggregate>(jobTypeId))
				.Select(e => e.EventData)
				.ToArray();

			if (aggEventDatas.Length == 0)
				return null;

			var jobType = new JobTypeAggregate();
			jobType.ApplyEvents(aggEventDatas);

			return jobType;
		}

		private readonly IEventStore _eventStore;
	}
}
