using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cyrka.api.domain.projects.proto
{
	public class InMemoryProjectRepository : IProjectRepository
	{

		public InMemoryProjectRepository()
		{
			_list = new List<Project>();
		}

		public Task<Project[]> FindAll()
		{
			return Task.FromResult(_list.ToArray());
		}

		public Task<Project> GetById(string id)
		{
			return Task.FromResult(_list.FirstOrDefault(p => p.Id == id));
		}

		public Task RemoveAsync(Project project)
		{
			_list.RemoveAll(p => p.Id == project.Id);
			return Task.CompletedTask;

		}

		public Task StoreAsync(Project project)
		{
			_list.RemoveAll(p => p.Id == project.Id);
			_list.Add(project);
			return Task.CompletedTask;
		}

		private readonly List<Project> _list;
	}
}