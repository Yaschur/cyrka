using System.Linq;
using cyrka.api.common.projections;
using MongoDB.Driver;

namespace cyrka.api.infra.stores.projections
{
	public class MongoQueryableProjection<TView> : IQueryableProjection<TView>
		where TView : IView
	{
		public MongoQueryableProjection(IMongoDatabase mongoDatabase)
		{
			var collectionName = typeof(TView).Name;
			_mongoCollection = mongoDatabase.GetCollection<TView>(collectionName);
		}

		public IQueryable<TView> AsQueryable()
		{
			return _mongoCollection.AsQueryable();
		}

		private readonly IMongoCollection<TView> _mongoCollection;
	}
}
