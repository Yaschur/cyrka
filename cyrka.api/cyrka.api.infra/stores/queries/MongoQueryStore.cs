using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using cyrka.api.domain;
using MongoDB.Driver;

namespace cyrka.api.infra.stores.queries
{
	public class MongoQueryStore : IQueryStore
	{
		public MongoQueryStore(IMongoDatabase mongoDatabase)
		{
			_mDb = mongoDatabase;
		}
		public IQueryable<TProjection> AsQueryable<TProjection>()
		{
			try
			{
				var collectionName = nameof(TProjection);
				var collection = _mDb.GetCollection<TProjection>(collectionName);
				return collection.AsQueryable();
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public async Task Upsert<TProjection>(TProjection projectionValue, Expression<Func<TProjection, bool>> filter)
		{
			try
			{
				var collectionName = nameof(TProjection);
				var collection = _mDb.GetCollection<TProjection>(collectionName);
				await collection.ReplaceOneAsync(filter, projectionValue, new UpdateOptions { IsUpsert = true });
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		private readonly IMongoDatabase _mDb;
	}

}
