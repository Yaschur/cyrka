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
using cyrka.api.domain.projects.commands.setPayments;
using Microsoft.AspNetCore.Authorization;

namespace cyrka.api.web.controllers
{
	[Route("projects"), Authorize]
	public class ProjectController : Controller
	{
		public ProjectController(
			ProjectCommandService projectCommandService,
			IQueryStore queryStore
		)
		{
			_projectCommandService = projectCommandService;
			_queryStore = queryStore;
		}

		[HttpPost]
		public async Task<IActionResult> RegisterProject()
		{
			var command = new RegisterProject();
			var result = await _projectCommandService.Do(command);
			return result;
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
			var result = await _projectCommandService.Do(command, projectId);
			return result;
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
			var result = await _projectCommandService.Do(command, projectId);
			return result;
		}

		[HttpPut("{projectId}/jobs/{jobTypeId}")]
		public async Task<IActionResult> ChangeJob(string projectId, string jobTypeId, [FromBody] JobVolumeInfo body)
		{
			var command = new ChangeJob(
				jobTypeId,
				body.RatePerUnit,
				body.Amount
			);
			var result = await _projectCommandService.Do(command, projectId);
			return result;
		}

		[HttpPost("{projectId}/status")]
		public async Task<IActionResult> SetStatus(string projectId, [FromBody] StatusInfo body)
		{
			var command = new SetStatus(body.Status);
			var result = await _projectCommandService.Do(command, projectId);
			return result;
		}

		[HttpPost("{projectId}/payments")]
		public async Task<IActionResult> SetPayments(string projectId, [FromBody] PaymentsInfo body)
		{
			var command = new SetPayments(body.TranslatorPayment, body.EditorPayment);
			var result = await _projectCommandService.Do(command, projectId);
			return result;
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

		private readonly ProjectCommandService _projectCommandService;
		private readonly IQueryStore _queryStore;
	}
}
