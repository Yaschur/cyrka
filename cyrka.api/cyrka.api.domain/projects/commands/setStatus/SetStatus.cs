namespace cyrka.api.domain.projects.commands.setStatus
{
	public class SetStatus
	{
		public readonly ProjectStatus Status;

		public SetStatus(ProjectStatus status)
		{
			Status = status;
		}
	}
}
