using System;
using System.Linq.Expressions;
using cyrka.api.common.queries;
using cyrka.api.domain.projects.commands;
using cyrka.api.domain.projects.commands.register;
using cyrka.api.domain.projects.commands.setCustomer;
using cyrka.api.domain.projects.commands.setEpisode;
using cyrka.api.domain.projects.commands.setTitle;

namespace cyrka.api.domain.projects.queries
{
	public class ProjectPlainRegistrator
	{
		public void RegisterIn(QueryEventProcessor processor)
		{
			processor.RegisterEventProcessing<ProjectRegistered, ProjectPlain>(UpdateByEventData, IdFilterByEventData);
			processor.RegisterEventProcessing<CustomerSet, ProjectPlain>(UpdateByEventData, IdFilterByEventData);
			processor.RegisterEventProcessing<TitleSet, ProjectPlain>(UpdateByEventData, IdFilterByEventData);
			processor.RegisterEventProcessing<EpisodeSet, ProjectPlain>(UpdateByEventData, IdFilterByEventData);
		}

		public Expression<Func<ProjectPlain, bool>> IdFilterByEventData(ProjectEventData eventData)
		{
			return p => p.Id == eventData.AggregateId;
		}

		public ProjectPlain UpdateByEventData(ProjectRegistered eventData, ProjectPlain source)
		{
			return new ProjectPlain
			{
				Id = eventData.AggregateId
			};
		}

		public ProjectPlain UpdateByEventData(CustomerSet eventData, ProjectPlain source)
		{
			return new ProjectPlain
			{
				Id = source.Id,
				Customer = new ProjectCustomer { Id = eventData.CustomerId, Name = eventData.CustomerName },
				Episode = source.Episode,
				Title = source.Title
			};
		}

		public ProjectPlain UpdateByEventData(TitleSet eventData, ProjectPlain source)
		{
			return new ProjectPlain
			{
				Id = source.Id,
				Customer = source.Customer,
				Episode = source.Episode,
				Title = new ProjectTitle { Id = eventData.TitleId, Name = eventData.TitleName, NumberOfEpisodes = eventData.NumberOfEpisodes }
			};
		}

		public ProjectPlain UpdateByEventData(EpisodeSet eventData, ProjectPlain source)
		{
			return new ProjectPlain
			{
				Id = source.Id,
				Customer = source.Customer,
				Episode = new ProjectEpisode { Number = eventData.Number, Duration = eventData.Duration },
				Title = source.Title
			};
		}
	}
}
