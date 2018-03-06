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
using cyrka.api.domain.projects.commands.changeJob;
using cyrka.api.domain.projects.commands.setStatus;

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
				body.CustomerId,
				body.CustomerName,
				body.TitleId,
				body.TitleName,
				body.TotalEpisodes,
				body.EpisodeNumber,
				body.EpisodeDuration
			);
			var result = await _projectService.Do(projectId, command);

			if (result == null)
				return NotFound();

			return Ok(result);
		}

		[HttpPost("{projectId}/job")]
		public async Task<IActionResult> SetJob(string projectId, [FromBody] JobInfo body)
		{
			var command = new SetJob(
				body.JobTypeId,
				body.JobTypeName,
				body.UnitName,
				body.RatePerUnit,
				body.Amount
			);
			var result = await _projectService.Do(projectId, command);

			if (result == null)
				return NotFound();

			return Ok(result);
		}

		[HttpPost("{projectId}/jobs")]
		public async Task<IActionResult> SetJobs(string projectId, [FromBody] JobInfo[] bodies)
		{
			var results = new List<WebAnswerBody>();
			foreach (var body in bodies)
			{
				var command = new SetJob(
					body.JobTypeId,
					body.JobTypeName,
					body.UnitName,
					body.RatePerUnit,
					body.Amount
				);
				var result = await _projectService.Do(projectId, command);
				results.Add(result);
			}

			if (results.All(r => r == null))
				return NotFound();

			return Ok(results.First(r => r != null));
		}

		[HttpPut("{projectId}/jobs/{jobTypeId}")]
		public async Task<IActionResult> ChangeJob(string projectId, string jobTypeId, [FromBody] JobVolumeInfo body)
		{
			var command = new ChangeJob(
				jobTypeId,
				body.RatePerUnit,
				body.Amount
			);
			var result = await _projectService.Do(projectId, command);

			if (result == null)
				return NotFound();

			return Ok(result);
		}

		[HttpPost("{projectId}/status")]
		public async Task<IActionResult> SetStatus(string projectId, [FromBody] StatusInfo body)
		{
			var command = new SetStatus(body.Status);
			var result = await _projectService.Do(projectId, command);

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
