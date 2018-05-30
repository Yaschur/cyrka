using System;
using System.Linq;
using System.Linq.Expressions;
using cyrka.api.common.queries;
using cyrka.api.domain.projects.commands;
using cyrka.api.domain.projects.commands.changeJob;
using cyrka.api.domain.projects.commands.register;
using cyrka.api.domain.projects.commands.setJob;
using cyrka.api.domain.projects.commands.setPayments;
using cyrka.api.domain.projects.commands.setProduct;
using cyrka.api.domain.projects.commands.setStatus;
using cyrka.api.domain.projects.events;

namespace cyrka.api.domain.projects.queries
{
	public class ProjectPlainRegistrator
	{
		public void RegisterIn(QueryEventProcessor processor)
		{
			processor.RegisterEventProcessing<ProjectRegistered, ProjectPlain>(UpdateByEventData, IdFilterByEventData);
			processor.RegisterEventProcessing<ProductSet, ProjectPlain>(UpdateByEventData, IdFilterByEventData);
			processor.RegisterEventProcessing<JobSet, ProjectPlain>(UpdateByEventData, IdFilterByEventData);
			processor.RegisterEventProcessing<JobChanged, ProjectPlain>(UpdateByEventData, IdFilterByEventData);
			processor.RegisterEventProcessing<StatusSet, ProjectPlain>(UpdateByEventData, IdFilterByEventData);
			processor.RegisterEventProcessing<PaymentsSet, ProjectPlain>(UpdateByEventData, IdFilterByEventData);
			processor.RegisterEventProcessing<IncomeChanged, ProjectPlain>(UpdateByEventData, IdFilterByEventData);
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

		public ProjectPlain UpdateByEventData(ProductSet eventData, ProjectPlain source)
		{
			return new ProjectPlain
			{
				Id = source.Id,
				Product = new ProductState
				{
					CustomerId = eventData.CustomerId,
					CustomerName = eventData.CustomerName,
					TitleId = eventData.TitleId,
					TitleName = eventData.TitleName,
					TotalEpisodes = eventData.TotalEpisodes,
					EpisodeNumber = eventData.EpisodeNumber,
					EpisodeDuration = eventData.EpisodeDuration,
				},
				Jobs = source.Jobs,
				Status = source.Status,
				Payments = source.Payments,
				Money = source.Money,
			};
		}

		public ProjectPlain UpdateByEventData(JobSet eventData, ProjectPlain source)
		{
			var jobs = source.Jobs
				.Where(j => j.JobTypeId != eventData.JobTypeId)
				.ToList();

			jobs.Add(
				new JobState
				{
					Amount = eventData.Amount,
					JobTypeId = eventData.JobTypeId,
					JobTypeName = eventData.JobTypeName,
					RatePerUnit = eventData.RatePerUnit,
					UnitName = eventData.UnitName,
				}
			);

			return new ProjectPlain
			{
				Id = source.Id,
				Product = source.Product,
				Jobs = jobs,
				Status = source.Status,
				Payments = source.Payments,
				Money = source.Money,
			};
		}

		public ProjectPlain UpdateByEventData(JobChanged eventData, ProjectPlain source)
		{
			var exJob = source.Jobs
				.FirstOrDefault(j => j.JobTypeId == eventData.JobTypeId);
			if (exJob == null)
				return null;
			var jobs = source.Jobs
				.Where(j => j.JobTypeId != eventData.JobTypeId)
				.ToList();

			jobs.Add(
				new JobState
				{
					Amount = eventData.Amount,
					JobTypeId = exJob.JobTypeId,
					JobTypeName = exJob.JobTypeName,
					RatePerUnit = eventData.RatePerUnit,
					UnitName = exJob.UnitName,
				}
			);

			return new ProjectPlain
			{
				Id = source.Id,
				Product = source.Product,
				Jobs = jobs,
				Status = source.Status,
				Payments = source.Payments,
				Money = source.Money,
			};
		}

		public ProjectPlain UpdateByEventData(StatusSet eventData, ProjectPlain source)
		{
			return new ProjectPlain
			{
				Id = source.Id,
				Product = source.Product,
				Jobs = source.Jobs,
				Status = eventData.Status,
				Payments = source.Payments,
				Money = source.Money,
			};
		}

		public ProjectPlain UpdateByEventData(PaymentsSet eventData, ProjectPlain source)
		{
			return new ProjectPlain
			{
				Id = source.Id,
				Product = source.Product,
				Jobs = source.Jobs,
				Status = source.Status,
				Payments = new PaymentsState
				{
					EditorPayment = eventData.EditorPayment,
					TranslatorPayment = eventData.TranslatorPayment
				},
				Money = source.Money,
			};
		}

		public ProjectPlain UpdateByEventData(IncomeChanged eventData, ProjectPlain source)
		{
			return new ProjectPlain
			{
				Id = source.Id,
				Product = source.Product,
				Jobs = source.Jobs,
				Status = source.Status,
				Payments = source.Payments,
				Money = new IncomeStatement
				{
					Income = eventData.IsExpenses ? (source.Money?.Income ?? 0) : eventData.Value,
					Expenses = eventData.IsExpenses ? eventData.Value : (source.Money?.Expenses ?? 0)
				},
			};
		}
	}
}
