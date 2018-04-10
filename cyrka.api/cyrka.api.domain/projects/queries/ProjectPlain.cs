using System.Collections.Generic;
using System.Linq;

namespace cyrka.api.domain.projects.queries
{
	public class ProjectPlain
	{
		public string Id { get; set; }

		public ProjectStatus Status { get; set; }

		public ProductState Product { get; set; }

		public List<JobState> Jobs { get; set; } = new List<JobState>();

		public PaymentsState Payments { get; set; }

		public decimal Income
		{
			get
			{
				return Jobs.Sum(j => j.Amount * j.RatePerUnit);
			}
		}

		public decimal Expenses
		{
			get
			{
				return (Payments?.EditorPayment ?? 0) + (Payments?.TranslatorPayment ?? 0);
			}
		}
	}
}
