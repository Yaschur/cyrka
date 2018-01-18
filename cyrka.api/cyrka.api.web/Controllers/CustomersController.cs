using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cyrka.api.common.events;
using cyrka.api.common.queries;
using cyrka.api.domain.customers;
using cyrka.api.domain.customers.commands.register;
using cyrka.api.domain.customers.commands.registerTitle;
using cyrka.api.domain.customers.queries;
using cyrka.api.infra.nexter;
using cyrka.api.web.Models.Customers;
using Microsoft.AspNetCore.Mvc;

namespace cyrka.api.web.Controllers
{
	[Route("customers")]
	public class CustomersController : Controller
	{
		const string EventChannelKey = "events";

		public CustomersController(
			NexterGenerator nexterGenerator,
			IEventStore eventStore,
			IQueryStore queryStore,
			CustomerAggregateRepository customerRepository
		)
		{
			_nexter = nexterGenerator;
			_eventStore = eventStore;
			_queryStore = queryStore;
			_customerRepository = customerRepository;
		}

		[HttpPost]
		public async Task<IActionResult> RegisterCustomer([FromBody]CustomerInfo value)
		{
			var command = new RegisterCustomer(value.Name, value.Description);
			var commandHandler = new RegisterCustomerHandler();
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

		[HttpPost("{id}")]
		public async Task<IActionResult> RegisterTitle(string id, [FromBody]TitleInfo value)
		{
			var customerAggregate = await _customerRepository.GetById(id);
			if (customerAggregate == null)
				return NotFound();
			var command = new RegisterTitle(id, value.Name, value.NumberOfSeries, value.Description);
			var commandHandler = new RegisterTitleHandler();
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

		[HttpGet]
		public IEnumerable<CustomerPlain> Get() =>
			_queryStore
				.AsQueryable<CustomerPlain>()
				.ToList();

		private readonly NexterGenerator _nexter;
		private readonly IEventStore _eventStore;
		private readonly IQueryStore _queryStore;
		CustomerAggregateRepository _customerRepository;
	}
}
