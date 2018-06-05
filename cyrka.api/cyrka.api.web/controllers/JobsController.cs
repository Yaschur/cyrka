using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cyrka.api.common.events;
using cyrka.api.common.queries;
using cyrka.api.domain.jobs.commands.change;
using cyrka.api.domain.jobs.commands.register;
using cyrka.api.domain.jobs.queries;
using cyrka.api.web.models.jobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using cyrka.api.web.services;

namespace cyrka.api.web.controllers
{
	[Route("jobs"), Authorize]
	public class JobsController : Controller
	{
		public JobsController(IQueryStore queryStore)
		{
			_queryStore = queryStore;
		}

		[HttpPost("types")]
		public async Task<IActionResult> RegisterJobType(
			[FromBody] JobTypeInfo value,
			[FromServices] JobTypeCommandService<RegisterJobType> jobTypeCommandService
		)
		{
			var command = new RegisterJobType(value.Name, value.Description, value.Unit, value.Rate);
			var result = await jobTypeCommandService.Do(command);
			return result;
		}

		[HttpPut("types/{jobTypeId}")]
		public async Task<IActionResult> ChangeJobType(
			string jobTypeId,
			[FromBody]JobTypeInfo value,
			[FromServices] JobTypeCommandService<ChangeJobType> jobTypeCommandService
		)
		{
			var command = new ChangeJobType(value.Name, value.Description, value.Unit, value.Rate);
			var result = await jobTypeCommandService.Do(command, jobTypeId);
			return result;
		}

		[HttpGet("types")]
		public IEnumerable<JobTypePlain> Get() =>
			_queryStore
				.AsQueryable<JobTypePlain>()
				.ToList();

		[HttpGet("types/{jobTypeId}")]
		public IActionResult Details(string jobTypeId)
		{
			var jobType = _queryStore
				.AsQueryable<JobTypePlain>()
				.FirstOrDefault(c => c.Id == jobTypeId);

			if (jobType == null)
				return NotFound();

			return Ok(jobType);
		}

		private readonly IQueryStore _queryStore;
	}
}
