using System.Collections.Generic;

namespace cyrka.api.domain.customers.projections.viewModels
{
	public class CustomerShortView
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int TitlesCount { get; set; }
	}
}
