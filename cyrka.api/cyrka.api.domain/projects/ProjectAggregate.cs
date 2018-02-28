using System.Collections.Generic;
using System.Linq;
using cyrka.api.common.events;
using cyrka.api.domain.projects.commands.register;
using cyrka.api.domain.projects.commands.setJob;
using cyrka.api.domain.projects.commands.setProduct;

namespace cyrka.api.domain.projects
{
	public class ProjectAggregate
	{
		public ProjectState State { get; } = new ProjectState();

		public void ApplyEvents(IEnumerable<EventData> aggEventDatas)
		{
			if (aggEventDatas == null)
				return;

			foreach (var eventData in aggEventDatas)
			{
				switch (eventData)
				{
					case ProjectRegistered projectRegistered:
						ApplyEvent(projectRegistered);
						break;
					case ProductSet productSet:
						ApplyEvent(productSet);
						break;
					case JobSet jobSet:
						ApplyEvent(jobSet);
						break;
				}
			}
		}

		private void ApplyEvent(ProjectRegistered projectRegistered)
		{
			State.ProjectId = projectRegistered.AggregateId;
		}

		private void ApplyEvent(ProductSet productSet)
		{
			State.Product = new ProductState
			{
				CustomerId = productSet.CustomerId,
				CustomerName = productSet.CustomerName,
				TitleId = productSet.TitleId,
				TitleName = productSet.TitleName,
				TotalEpisodes = productSet.TotalEpisodes,
				EpisodeNumber = productSet.EpisodeNumber,
				EpisodeDuration = productSet.EpisodeDuration
			};
		}

		private void ApplyEvent(JobSet jobSet)
		{
			var jobList = State.Jobs.ToList();
			jobList.RemoveAll(j => j.JobTypeId == jobSet.JobTypeId);
			jobList.Add(
				new JobState
				{
					JobTypeId = jobSet.JobTypeId,
					JobTypeName = jobSet.JobTypeName,
					UnitName = jobSet.UnitName,
					RatePerUnit = jobSet.RatePerUnit,
					Amount = jobSet.Amount,
				}
			);
			State.Jobs = jobList.ToArray();
		}
	}
}
