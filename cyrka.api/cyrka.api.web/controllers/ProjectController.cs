using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cyrka.api.common.queries;
using cyrka.api.domain.projects.commands.register;
using cyrka.api.domain.projects.queries;
using Microsoft.AspNetCore.Mvc;
using cyrka.api.web.services;

namespace cyrka.api.web.controllers
{
	[Route("projects")]
	public class ProjectController : Controller
	{
		const string EventChannelKey = "events";

		public ProjectController(
			ProjectService projectService,
			IQueryStore queryStore
		)
		{
			_queryStore = queryStore;
			_projectService = projectService;
		}

		[HttpPost]
		public async Task<IActionResult> RegisterProject()
		{
			var command = new RegisterProject();
			var result = await _projectService.Do(command);

			return Ok(result);
		}

		[HttpGet]
		public IEnumerable<ProjectPlain> Get() =>
			_queryStore
				.AsQueryable<ProjectPlain>()
				.ToList();

		private readonly ProjectService _projectService;
		private readonly IQueryStore _queryStore;
	}
}
