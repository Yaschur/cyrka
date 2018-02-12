namespace cyrka.api.domain.projects.commands.register
{
	public class ProjectRegistered : ProjectEventData
	{
		public ProjectRegistered(string projectId)
			: base(projectId)
		{
		}
	}
}
