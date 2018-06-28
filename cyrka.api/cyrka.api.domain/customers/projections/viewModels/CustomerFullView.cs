using System.Collections.Generic;

namespace cyrka.api.domain.customers.projections.viewModels
{
	public class CustomerFullView : CustomerShortView
	{
		public List<TitleView> Titles { get; set; }
	}
}
