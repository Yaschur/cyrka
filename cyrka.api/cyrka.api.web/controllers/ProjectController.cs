using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cyrka.api.common.queries;
using cyrka.api.domain.projects.commands.register;
using cyrka.api.domain.projects.queries;
using Microsoft.AspNetCore.Mvc;
using cyrka.api.web.services;
using cyrka.api.web.models.projects;
using cyrka.api.domain.projects.commands.setProduct;
using cyrka.api.domain.projects.commands.setJob;

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

		[HttpPost("{projectId}/product")]
		public async Task<IActionResult> SetProduct(string projectId, [FromBody] ProductInfo body)
		{
			var command = new SetProduct(
				projectId,
				body.CustomerId,
				body.CustomerName,
				body.TitleId,
				body.TitleName,
				body.TotalEpisodes,
				body.EpisodeNumber,
				body.EpisodeDuration
			);
			var result = await _projectService.Do(command);

			if (result == null)
				return NotFound();

			return Ok(result);
		}

		[HttpPost("{projectId}/job")]
		public async Task<IActionResult> SetJob(string projectId, [FromBody] JobInfo body)
		{
			var command = new SetJob(
				projectId,
				body.JobTypeId,
				body.JobTypeName,
				body.UnitName,
				body.RatePerUnit,
				body.Amount
			);
			var result = await _projectService.Do(command);

			if (result == null)
				return NotFound();

			return Ok(result);
		}

		[HttpGet]
		public IEnumerable<ProjectPlain> Get() =>
			_queryStore
				.AsQueryable<ProjectPlain>()
				.ToList();

		[HttpGet("{projectId}")]
		public IActionResult Details(string projectId)
		{
			var project = _queryStore
				.AsQueryable<ProjectPlain>()
				.FirstOrDefault(c => c.Id == projectId);

			if (project == null)
				return NotFound();

			return Ok(project);
		}

		private readonly ProjectService _projectService;
		private readonly IQueryStore _queryStore;
	}
}
