using cyrka.api.common.events;

namespace cyrka.api.domain.jobs.commands
{
	public abstract class JobTypeEventData : EventData
	{
		public override string AggregateType => nameof(JobTypeAggregate);

		public override string AggregateId { get; }

		public JobTypeEventData(string jobTypeId)
		{
			AggregateId = jobTypeId;
		}
	}
}
