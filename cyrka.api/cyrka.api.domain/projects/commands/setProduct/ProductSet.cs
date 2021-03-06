namespace cyrka.api.domain.projects.commands.setProduct
{
	public class ProductSet : ProjectEventData
	{
		public readonly string CustomerId;
		public readonly string CustomerName;
		public readonly string TitleId;
		public readonly string TitleName;
		public readonly int TotalEpisodes;
		public readonly int EpisodeNumber;
		public readonly int EpisodeDuration;

		public ProductSet(
			string projectId,
			string customerId,
			string customerName,
			string titleId,
			string titleName,
			int totalEpisodes,
			int episodeNumber,
			int episodeDuration
		)
			: base(projectId)
		{
			CustomerId = customerId;
			CustomerName = customerName;
			TitleId = titleId;
			TitleName = titleName;
			TotalEpisodes = totalEpisodes;
			EpisodeNumber = episodeNumber;
			EpisodeDuration = episodeDuration;
		}
	}
}
