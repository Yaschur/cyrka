using System;

namespace cyrka.api.domain.projects.proto
{
	public class Project
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public ProjectType Type { get; set; }
		public DateTime? PlannedFinish { get; set; }
		public DateTime? EstimatedFinish { get; set; }
	}
}