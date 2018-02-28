namespace cyrka.api.domain.projects.commands.changeJob
{
	public class JobChanged : ProjectEventData
	{
		public readonly string JobTypeId;
		public readonly decimal RatePerUnit;
		public readonly uint Amount;

		public JobChanged(
			string projectId,
			string jobTypeId,
			decimal ratePerUnit,
			uint amount
		) : base(projectId)
		{
			JobTypeId = jobTypeId;
			RatePerUnit = ratePerUnit;
			Amount = amount;
		}
	}
}
