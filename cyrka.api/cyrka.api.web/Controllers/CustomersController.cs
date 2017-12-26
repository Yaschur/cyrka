using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cyrka.api.common.events;
using cyrka.api.common.queries;
using cyrka.api.domain.customers.commands.register;
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
			IQueryStore queryStore
		)
		{
			_nexter = nexterGenerator;
			_eventStore = eventStore;
			_queryStore = queryStore;
		}

		[HttpPost]
		public async Task Post([FromBody]CustomerInfo value)
		{
			var command = new CustomerRegister(value.Name, value.Description);
			var commandHandler = new CustomerRegisterHandler();
			var eventDatas = commandHandler.Handle(command);

			foreach (var data in eventDatas)
			{
				var lastEventId = await _eventStore.GetLastStoredId();
				var id = await _nexter.GetNextNumber(EventChannelKey, lastEventId);
				var createdAt = DateTime.UtcNow;
				var newEvent = new Event(id, createdAt, data);
				await _eventStore.Store(newEvent);
			}
		}

		[HttpGet]
		public IEnumerable<CustomerPlain> Get() =>
			_queryStore
				.AsQueryable<CustomerPlain>()
				.ToList();

		private readonly NexterGenerator _nexter;
		private readonly IEventStore _eventStore;
		private readonly IQueryStore _queryStore;
	}
}
