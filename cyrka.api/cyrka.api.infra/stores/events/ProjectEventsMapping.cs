using cyrka.api.domain.projects.commands;
using cyrka.api.domain.projects.commands.register;
using cyrka.api.domain.projects.commands.setProduct;
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
		}
	}
}
