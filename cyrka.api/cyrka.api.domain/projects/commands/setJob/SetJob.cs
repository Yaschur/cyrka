namespace cyrka.api.domain.projects.commands.setJob
{
	public class SetJob
	{
		public readonly string ProjectId;
		public readonly string JobTypeId;
		public readonly string JobTypeName;
		public readonly string UnitName;
		public readonly decimal RatePerUnit;
		public readonly uint Amount;

		public SetJob(
			string projectId,
			string jobTypeId,
			string jobTypeName,
			string unitName,
			decimal ratePerUnit,
			uint amount
		)
		{
			ProjectId = projectId;
			JobTypeId = jobTypeId;
			JobTypeName = jobTypeName;
			UnitName = unitName;
			RatePerUnit = ratePerUnit;
			Amount = amount;
		}
	}
}
