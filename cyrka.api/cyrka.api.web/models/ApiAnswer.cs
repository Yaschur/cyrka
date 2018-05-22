namespace cyrka.api.web.models
{
	public class ApiAnswer
	{
		public string ResourceType { get; set; }
		public string ResourceId { get; set; }
		public Error Error { get; set; }
	}
}
