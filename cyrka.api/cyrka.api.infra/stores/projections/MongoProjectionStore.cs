using System.Linq;
using System.Threading.Tasks;
using cyrka.api.common.projections;
using MongoDB.Driver;

namespace cyrka.api.infra.stores.projections
{
	public class MongoProjectionStore<TView> : IProjectionStore<TView>
		where TView : IView
	{
		public MongoProjectionStore(IMongoDatabase mongoDatabase)
		{
			var collectionName = typeof(TView).Name;
			_mongoCollection = mongoDatabase.GetCollection<TView>(collectionName);
		}

		public TView GetById(string id)
		{
			try
			{
				return _mongoCollection
					.AsQueryable()
					.FirstOrDefault(v => v.Id == id);
			}
			catch (System.Exception)
			{
				throw;
			}
		}

		public async Task RemoveAsync(TView view)
		{
			try
			{
				await _mongoCollection.DeleteOneAsync(v => view.Id == v.Id);
			}
			catch (System.Exception)
			{
				throw;
			}
		}

		public async Task StoreAsync(TView view)
		{
			try
			{
				await _mongoCollection.ReplaceOneAsync(
				 v => v.Id == view.Id,
				 view,
				 new UpdateOptions { IsUpsert = true }
			 );
			}
			catch (System.Exception)
			{
				throw;
			}
		}

		private readonly IMongoCollection<TView> _mongoCollection;
	}
}
