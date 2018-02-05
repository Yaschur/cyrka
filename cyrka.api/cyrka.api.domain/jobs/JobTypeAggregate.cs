using System;
using System.Collections.Generic;
using cyrka.api.common.events;
using cyrka.api.domain.jobs.commands.change;
using cyrka.api.domain.jobs.commands.register;

namespace cyrka.api.domain.jobs
{
	public class JobTypeAggregate
	{
		public string Id { get; private set; }

		public string Name { get; private set; }

		public string Description { get; private set; }

		public JobTypeUnit Unit { get; private set; } = JobTypeUnit.Undefined;

		public decimal Rate { get; private set; }

		public void ApplyEvents(IEnumerable<EventData> aggEventDatas)
		{
			if (aggEventDatas == null)
				return;

			foreach (var eventData in aggEventDatas)
			{
				switch (eventData)
				{
					case JobTypeRegistered jobTypeRegistered:
						ApplyEvent(jobTypeRegistered);
						break;
					case JobTypeChanged jobTypeChanged:
						ApplyEvent(jobTypeChanged);
						break;
				}
			}
		}

		private void ApplyEvent(JobTypeRegistered jobTypeEvent)
		{
			// if (Id == null)
			// 	return;
			Id = jobTypeEvent.AggregateId;
			Name = jobTypeEvent.Name;
			Description = jobTypeEvent.Description;
			Unit = jobTypeEvent.Unit;
			Rate = jobTypeEvent.Rate;
		}

		private void ApplyEvent(JobTypeChanged jobTypeEvent)
		{
			Name = jobTypeEvent.Name;
			Description = jobTypeEvent.Description;
			Unit = jobTypeEvent.Unit;
			Rate = jobTypeEvent.Rate;
		}
	}
}
