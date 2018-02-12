using System;
using System.Linq.Expressions;
using cyrka.api.common.queries;
using cyrka.api.domain.projects.commands;
using cyrka.api.domain.projects.commands.register;

namespace cyrka.api.domain.projects.queries
{
	public class ProjectPlainRegistrator
	{
		public void RegisterIn(QueryEventProcessor processor)
		{
			processor.RegisterEventProcessing<ProjectRegistered, ProjectPlain>(UpdateByEventData, IdFilterByEventData);
			// processor.RegisterEventProcessing<JobTypeChanged, JobTypePlain>(UpdateByEventData, IdFilterByEventData);
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
	}
}
