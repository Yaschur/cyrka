namespace cyrka.api.domain.projects.commands.changeJob
{
	public class ChangeJob
	{
		public readonly string JobTypeId;
		public readonly decimal RatePerUnit;
		public readonly uint Amount;

		public ChangeJob(
			string jobTypeId,
			decimal ratePerUnit,
			uint amount
		)
		{
			JobTypeId = jobTypeId;
			RatePerUnit = ratePerUnit;
			Amount = amount;
		}
	}
}
