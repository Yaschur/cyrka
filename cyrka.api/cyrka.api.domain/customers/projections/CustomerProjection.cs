using System.Linq;
using cyrka.api.common.projections;
using cyrka.api.domain.customers.projections.viewModels;

namespace cyrka.api.domain.customers.projections
{
	public class CustomerProjection
	{
		public CustomerProjection(IProjectionStore projectionStore)
		{
			_projectionStore = projectionStore;
		}

		public TitleView[] FindTitles(string customerId) =>
			_projectionStore.Query<CustomerFullView>()
				.Where(c => c.Id == customerId)
				.SelectMany(c => c.Titles)
				.ToArray();

		private readonly IProjectionStore _projectionStore;
	}
}
