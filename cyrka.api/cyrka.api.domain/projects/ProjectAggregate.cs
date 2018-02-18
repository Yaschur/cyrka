using System.Collections.Generic;
using cyrka.api.common.events;
using cyrka.api.domain.projects.commands.register;
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
	}
}
