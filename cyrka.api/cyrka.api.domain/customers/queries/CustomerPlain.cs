using System.Collections.Generic;

namespace cyrka.api.domain.customers.queries
{
	public class CustomerPlain
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public List<TitlePlain> Titles { get; set; } = new List<TitlePlain>();
	}
}
