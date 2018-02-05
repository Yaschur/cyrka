using System;
using System.Linq.Expressions;
using cyrka.api.common.queries;
using cyrka.api.domain.jobs.commands;
using cyrka.api.domain.jobs.commands.register;

namespace cyrka.api.domain.jobs.queries
{
	public class JobTypePlainRegistrator
	{
		public void RegisterIn(QueryEventProcessor processor)
		{
			processor.RegisterEventProcessing<JobTypeRegistered, JobTypePlain>(UpdateByEventData, IdFilterByEventData);
		}

		public Expression<Func<JobTypePlain, bool>> IdFilterByEventData(JobTypeEventData eventData)
		{
			return jt => jt.Id == eventData.AggregateId;
		}

		public JobTypePlain UpdateByEventData(JobTypeRegistered eventData, JobTypePlain source)
		{
			return new JobTypePlain
			{
				Id = eventData.AggregateId,
				Name = eventData.Name,
				Unit = eventData.Unit,
				Rate = eventData.Rate,
				Description = eventData.Description
			};
		}
	}
}
