namespace cyrka.api.domain.events
{
	public interface IEventDataSerializer
	{
		TData Deserialize<TData>(string stringifiedData);

		string Serialize<TData>(TData originalData);
	}
}
