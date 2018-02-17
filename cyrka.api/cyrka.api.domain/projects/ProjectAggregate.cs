using System.Collections.Generic;
using cyrka.api.common.events;
using cyrka.api.domain.projects.commands.register;
using cyrka.api.domain.projects.commands.setCustomer;
using cyrka.api.domain.projects.commands.setEpisode;
using cyrka.api.domain.projects.commands.setTitle;

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
					case CustomerSet customerSet:
						ApplyEvent(customerSet);
						break;
					case TitleSet titleSet:
						ApplyEvent(titleSet);
						break;
					case EpisodeSet episodeSet:
						ApplyEvent(episodeSet);
						break;
				}
			}
		}

		private void ApplyEvent(ProjectRegistered projectRegistered)
		{
			State.ProjectId = projectRegistered.AggregateId;
		}

		private void ApplyEvent(CustomerSet customerSet)
		{
			State.Customer = new ProjectCustomer { Id = customerSet.CustomerId, Name = customerSet.CustomerName };
		}

		private void ApplyEvent(TitleSet titleSet)
		{
			State.Title = new ProjectTitle { Id = titleSet.TitleId, Name = titleSet.TitleName, NumberOfEpisodes = titleSet.NumberOfEpisodes };
		}

		private void ApplyEvent(EpisodeSet episodeSet)
		{
			State.Episode = new ProjectEpisode { Number = episodeSet.Number, Duration = episodeSet.Duration };
		}
	}
}
