using cyrka.api.domain.projects.commands;
using cyrka.api.domain.projects.commands.changeJob;
using cyrka.api.domain.projects.commands.register;
using cyrka.api.domain.projects.commands.setJob;
using cyrka.api.domain.projects.commands.setPayments;
using cyrka.api.domain.projects.commands.setProduct;
using cyrka.api.domain.projects.commands.setStatus;
using cyrka.api.domain.projects.events;
using MongoDB.Bson.Serialization;

namespace cyrka.api.infra.stores.events
{
	public class ProjectEventsMapping : IDbMapping
	{
		public void DefineMaps()
		{
			BsonClassMap.RegisterClassMap<ProjectEventData>();

			BsonClassMap.RegisterClassMap<ProjectRegistered>(cm =>
			{
				cm.MapCreator(cr => new ProjectRegistered(cr.AggregateId));
			});

			BsonClassMap.RegisterClassMap<ProductSet>(cm =>
			{
				cm.MapField(cr => cr.CustomerId);
				cm.MapField(cr => cr.CustomerName);
				cm.MapField(cr => cr.TitleId);
				cm.MapField(cr => cr.TitleName);
				cm.MapField(cr => cr.TotalEpisodes);
				cm.MapField(cr => cr.EpisodeNumber);
				cm.MapField(cr => cr.EpisodeDuration);
				cm.MapCreator(cr => new ProductSet(
					cr.AggregateId,
					cr.CustomerId,
					cr.CustomerName,
					cr.TitleId,
					cr.TitleName,
					cr.TotalEpisodes,
					cr.EpisodeNumber,
					cr.EpisodeDuration
				));
			});

			BsonClassMap.RegisterClassMap<JobSet>(cm =>
			{
				cm.MapField(cr => cr.Amount);
				cm.MapField(cr => cr.JobTypeId);
				cm.MapField(cr => cr.JobTypeName);
				cm.MapField(cr => cr.RatePerUnit);
				cm.MapField(cr => cr.UnitName);
				cm.MapCreator(cr => new JobSet(
					cr.AggregateId,
					cr.JobTypeId,
					cr.JobTypeName,
					cr.UnitName,
					cr.RatePerUnit,
					cr.Amount
				));
			});

			BsonClassMap.RegisterClassMap<JobChanged>(cm =>
			{
				cm.MapField(cr => cr.Amount);
				cm.MapField(cr => cr.JobTypeId);
				cm.MapField(cr => cr.RatePerUnit);
				cm.MapCreator(cr => new JobChanged(
					cr.AggregateId,
					cr.JobTypeId,
					cr.RatePerUnit,
					cr.Amount
				));
			});

			BsonClassMap.RegisterClassMap<StatusSet>(cm =>
			{
				cm.MapField(cr => cr.Status);
				cm.MapCreator(cr => new StatusSet(cr.AggregateId, cr.Status));
			});

			BsonClassMap.RegisterClassMap<PaymentsSet>(cm =>
			{
				cm.MapField(cr => cr.EditorPayment);
				cm.MapField(cr => cr.TranslatorPayment);
				cm.MapCreator(cr => new PaymentsSet(cr.AggregateId, cr.TranslatorPayment, cr.EditorPayment));
			});

			BsonClassMap.RegisterClassMap<IncomeChanged>(cm =>
			{
				cm.MapField(cr => cr.IsExpenses);
				cm.MapField(cr => cr.Value);
				cm.MapCreator(cr => new IncomeChanged(cr.AggregateId, cr.Value, cr.IsExpenses));
			});
		}
	}
}
