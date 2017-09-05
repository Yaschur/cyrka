using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cyrka.api.domain.projects.proto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace cyrka.api.web.controllers
{
	[Route("v1/projects")]
	public class ProjectsController : Controller
	{
		public ProjectsController(IProjectRepository projectRepo, ILogger<ProjectsController> logger)
		{
			_projectRepo = projectRepo;
			_logger = logger;
		}

		[HttpGet("")]
		public async Task<IEnumerable<Project>> Get()
		{
			return await _projectRepo.FindAll();
		}

		[HttpGet("{id}")]
		public async Task<Project> Get(string id)
		{
			return await _projectRepo.GetById(id);
		}

		[HttpPost]
		public async Task Post([FromBody]Project project)
		{
			if (!ModelState.IsValid)
			{
				_logger.LogError(string.Join(", ", ModelState.Select(v => $"{v.Key}: {v.Value.RawValue}")));
				return;
			}
			await _projectRepo.StoreAsync(project);
		}

		// // PUT api/values/5
		// [HttpPut("{id}")]
		// public void Put(int id, [FromBody]string value)
		// {
		// }

		[HttpDelete("{id}")]
		public async Task Delete(string id)
		{
			var project = await _projectRepo.GetById(id);
			if (project != null)
				await _projectRepo.RemoveAsync(project);
		}

		private readonly IProjectRepository _projectRepo;
		private readonly ILogger<ProjectsController> _logger;
	}
}
