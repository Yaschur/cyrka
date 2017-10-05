namespace cyrka.api.domain.customers
{
	public class Episode
	{
		public Episode(int order, string number, string name, int timing)
		{
			Order = order;
			Number = number ?? string.Empty;
			Name = name ?? string.Empty;
			Timing = timing > 0 ? timing : 1;
		}

		public int Order { get; private set; }

		public string Number { get; private set; }

		public string Name { get; private set; }

		public int Timing { get; private set; }
	}
}