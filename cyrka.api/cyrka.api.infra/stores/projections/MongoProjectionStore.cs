using System.Threading.Tasks;
using cyrka.api.common.projections;

namespace cyrka.api.infra.stores.projections
{
	public class MongoProjectionStore<TView> : IProjectionStore<TView>
		where TView : IView
	{
		public TView GetById(string id)
		{
			throw new System.NotImplementedException();
		}

		public Task RemoveAsync(TView view)
		{
			throw new System.NotImplementedException();
		}

		public Task StoreAsync(TView view)
		{
			throw new System.NotImplementedException();
		}
	}
}
