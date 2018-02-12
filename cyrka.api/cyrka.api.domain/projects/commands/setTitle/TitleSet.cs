namespace cyrka.api.domain.projects.commands.setCustomer
{
	public class TitleSet : ProjectEventData
	{
		public readonly string TitleId;
		public readonly string TitleName;

		public TitleSet(string projectId, string titleId, string titleName)
			: base(projectId)
		{
			TitleId = titleId;
			TitleName = titleName;
		}
	}
}
