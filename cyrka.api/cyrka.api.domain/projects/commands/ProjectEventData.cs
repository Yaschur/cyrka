using cyrka.api.common.events;

namespace cyrka.api.domain.projects.commands
{
	public abstract class ProjectEventData : EventData
	{
		public override string AggregateType => nameof(ProjectAggregate);

		public override string AggregateId { get; }

		public ProjectEventData(string projectId)
		{
			AggregateId = projectId;
		}
	}
}
