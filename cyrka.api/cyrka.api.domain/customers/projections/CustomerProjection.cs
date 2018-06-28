using System.Linq;
using System.Threading.Tasks;
using cyrka.api.common.projections;
using cyrka.api.domain.customers.projections.viewModels;

namespace cyrka.api.domain.customers.projections
{
	public class CustomerProjection
	{
		public CustomerProjection(IProjectionStore projectionStore) =>
			_projectionStore = projectionStore;

		public CustomerFullView[] FindCustomersFull() =>
			_projectionStore.Query<CustomerFullView>()
			.ToArray();

		public CustomerFullView GetCustomerFull(string customerId) =>
			_projectionStore.Query<CustomerFullView>()
				.FirstOrDefault(c => c.Id == customerId);

		public CustomerShortView[] FindCustomersShort() =>
			_projectionStore.Query<CustomerFullView>()
				.Select(c => new CustomerShortView
				{
					Id = c.Id,
					Name = c.Name,
					Description = c.Description,
					TitlesCount = c.TitlesCount
				})
				.ToArray();

		public CustomerShortView GetCustomerShort(string customerId) =>
			_projectionStore.Query<CustomerFullView>()
				.Where(c => c.Id == customerId)
				.Select(c => new CustomerShortView
				{
					Id = c.Id,
					Name = c.Name,
					Description = c.Description,
					TitlesCount = c.TitlesCount
				})
				.FirstOrDefault();

		public TitleView[] FindTitles(string customerId) =>
			_projectionStore.Query<CustomerFullView>()
				.Where(c => c.Id == customerId)
				.SelectMany(c => c.Titles)
				.ToArray();

		public TitleView GetTitle(string customerId, string titleId) =>
			_projectionStore.Query<CustomerFullView>()
				.Where(c => c.Id == customerId)
				.SelectMany(c => c.Titles)
				.FirstOrDefault(t => t.Id == titleId);

		public async Task StoreAsync(CustomerFullView customer) =>
			await _projectionStore.Upsert(customer, p => p.Id == customer.Id);

		public async Task RemoveAsync(string customerId) =>
			await _projectionStore.Delete<CustomerFullView>(c => c.Id == customerId);

		private readonly IProjectionStore _projectionStore;
	}
}
