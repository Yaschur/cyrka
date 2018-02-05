using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cyrka.api.common.events;
using cyrka.api.common.queries;
using cyrka.api.domain.jobs;
using cyrka.api.domain.jobs.commands.change;
using cyrka.api.domain.jobs.commands.register;
using cyrka.api.domain.jobs.queries;
using cyrka.api.infra.nexter;
using cyrka.api.web.Models.Jobs;
using Microsoft.AspNetCore.Mvc;

namespace cyrka.api.web.Controllers
{
	[Route("jobs")]
	public class JobsController : Controller
	{
		const string EventChannelKey = "events";

		public JobsController(
			NexterGenerator nexterGenerator,
			IEventStore eventStore,
			IQueryStore queryStore,
			JobTypeAggregateRepository jobTypeRepository
		)
		{
			_nexter = nexterGenerator;
			_eventStore = eventStore;
			_queryStore = queryStore;
			_jobTypeRepository = jobTypeRepository;
		}

		[HttpPost("types")]
		public async Task<IActionResult> RegisterType([FromBody]JobTypeInfo value)
		{
			var command = new RegisterJobType(value.Name, value.Description, value.Unit, value.Rate);
			var commandHandler = new RegisterJobTypeHandler();
			var eventDatas = commandHandler.Handle(command);
			foreach (var data in eventDatas)
			{
				var lastEventId = await _eventStore.GetLastStoredId();
				var newEventId = await _nexter.GetNextNumber(EventChannelKey, lastEventId);
				var createdAt = DateTime.UtcNow;
				var newEvent = new Event(newEventId, createdAt, data);
				await _eventStore.Store(newEvent);
			}

			return Ok();
		}

		[HttpPut("types/{jobTypeId}")]
		public async Task<IActionResult> ChangeCustomer(string jobTypeId, [FromBody]JobTypeInfo value)
		{
			var jobTypeAggregate = await _jobTypeRepository.GetById(jobTypeId);
			if (jobTypeAggregate == null)
				return NotFound();
			var command = new ChangeJobType(jobTypeId, value.Name, value.Description, value.Unit, value.Rate);
			var commandHandler = new ChangeJobTypeHandler();
			var eventDatas = commandHandler.Handle(command);

			foreach (var data in eventDatas)
			{
				var lastEventId = await _eventStore.GetLastStoredId();
				var newEventId = await _nexter.GetNextNumber(EventChannelKey, lastEventId);
				var createdAt = DateTime.UtcNow;
				var newEvent = new Event(newEventId, createdAt, data);
				await _eventStore.Store(newEvent);
			}

			return Ok();
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

		private readonly NexterGenerator _nexter;
		private readonly IEventStore _eventStore;
		private readonly IQueryStore _queryStore;
		JobTypeAggregateRepository _jobTypeRepository;
	}
}
