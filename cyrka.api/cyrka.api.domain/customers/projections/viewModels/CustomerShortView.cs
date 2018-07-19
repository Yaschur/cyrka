using System.Collections.Generic;
using cyrka.api.common.projections;

namespace cyrka.api.domain.customers.projections.viewModels
{
	public class CustomerShortView : IView
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int TitlesCount { get; set; }
	}
}
