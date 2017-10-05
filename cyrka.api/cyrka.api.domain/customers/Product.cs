using System;

namespace cyrka.api.domain.customers
{
	public abstract class Product
	{
		protected Product(string id, string name, string description, int numberOfEpisodes)
		{
			Id = !string.IsNullOrWhiteSpace(id) ? id : throw new ArgumentException(nameof(id));
			Name = name ?? string.Empty;
			Description = description ?? string.Empty;
			NumberOfEpisodes = numberOfEpisodes > 0 ? numberOfEpisodes : 1;
		}

		public string Id { get; }

		public string Name { get; private set; }

		public string Description { get; private set; }

		public int NumberOfEpisodes { get; private set; }
	}
}