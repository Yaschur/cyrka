namespace cyrka.api.domain.projects.commands.setStatus
{
	public class StatusSet : ProjectEventData
	{
		public readonly ProjectStatus Status;

		public StatusSet(string projectId, ProjectStatus status)
			: base(projectId) => Status = status;
	}
}
