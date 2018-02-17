using cyrka.api.domain.projects.commands;
using cyrka.api.domain.projects.commands.register;
using cyrka.api.domain.projects.commands.setCustomer;
using cyrka.api.domain.projects.commands.setEpisode;
using cyrka.api.domain.projects.commands.setTitle;
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

			BsonClassMap.RegisterClassMap<CustomerSet>(cm =>
			{
				cm.MapField(cr => cr.CustomerId);
				cm.MapField(cr => cr.CustomerName);
				cm.MapCreator(cr => new CustomerSet(cr.AggregateId, cr.CustomerId, cr.CustomerName));
			});

			BsonClassMap.RegisterClassMap<TitleSet>(cm =>
			{
				cm.MapField(cr => cr.TitleId);
				cm.MapField(cr => cr.TitleName);
				cm.MapField(cr => cr.NumberOfEpisodes);
				cm.MapCreator(cr => new TitleSet(cr.AggregateId, cr.TitleId, cr.TitleName, cr.NumberOfEpisodes));
			});

			BsonClassMap.RegisterClassMap<EpisodeSet>(cm =>
			{
				cm.MapField(cr => cr.Number);
				cm.MapField(cr => cr.Duration);
				cm.MapCreator(cr => new EpisodeSet(cr.AggregateId, cr.Number, cr.Duration));
			});
		}
	}
}
