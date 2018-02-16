using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cyrka.api.common.events;
using cyrka.api.common.queries;
using cyrka.api.domain.projects;
using cyrka.api.domain.projects.commands.register;
using cyrka.api.domain.projects.queries;
using cyrka.api.common.generators;
using Microsoft.AspNetCore.Mvc;

namespace cyrka.api.web.controllers
{
	[Route("projects")]
	public class ProjectController : Controller
	{
		const string EventChannelKey = "events";

		public ProjectController(
			NexterGenerator nexterGenerator,
			IEventStore eventStore,
			IQueryStore queryStore,
			ProjectAggregateRepository projectRepository
		)
		{
			_nexter = nexterGenerator;
			_eventStore = eventStore;
			_queryStore = queryStore;
			_projectRepository = projectRepository;
		}

		[HttpPost]
		public async Task<IActionResult> RegisterProject()
		{
			var handler = new RegisterProjectHandler(_eventStore, _nexter);
			var eventData = await handler.Handle(new RegisterProject());
			var lastEventId = await _eventStore.GetLastStoredId();
			var newEventId = await _nexter.GetNextNumber(EventChannelKey, lastEventId);
			var createdAt = DateTime.UtcNow;
			var newEvent = new Event(newEventId, createdAt, eventData);
			await _eventStore.Store(newEvent);

			return Ok(new { Id = eventData.AggregateId, Type = eventData.AggregateType });
		}

		[HttpGet]
		public IEnumerable<ProjectPlain> Get() =>
			_queryStore
				.AsQueryable<ProjectPlain>()
				.ToList();

		private readonly NexterGenerator _nexter;
		private readonly IEventStore _eventStore;
		private readonly IQueryStore _queryStore;
		private readonly ProjectAggregateRepository _projectRepository;
	}
}
