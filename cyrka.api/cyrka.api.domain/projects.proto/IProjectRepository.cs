using System.Threading.Tasks;

namespace cyrka.api.domain.projects.proto
{
	public interface IProjectRepository
	{
		Task StoreAsync(Project project);
		Task RemoveAsync(Project project);
		Task<Project> GetById(string id);
		Task<Project[]> FindAll();
	}
}