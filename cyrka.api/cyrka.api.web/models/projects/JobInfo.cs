namespace cyrka.api.web.models.projects
{
	public class JobInfo
	{
		public string JobTypeId { get; set; }
		public string JobTypeName { get; set; }
		public string UnitName { get; set; }
		public decimal RatePerUnit { get; set; }
		public uint Amount { get; set; }
	}
}
