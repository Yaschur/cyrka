using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cyrka.api.common.events;
using cyrka.api.common.queries;
using cyrka.api.domain.customers;
using cyrka.api.domain.customers.commands.change;
using cyrka.api.domain.customers.commands.changeTitle;
using cyrka.api.domain.customers.commands.register;
using cyrka.api.domain.customers.commands.registerTitle;
using cyrka.api.domain.customers.commands.removeTitle;
using cyrka.api.domain.customers.commands.retire;
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

		[HttpPut("{customerId}")]
		public async Task<IActionResult> ChangeCustomer(string customerId, [FromBody]CustomerInfo value)
		{
			var customerAggregate = await _customerRepository.GetById(customerId);
			if (customerAggregate == null || customerAggregate.IsRetired)
				return NotFound();
			var command = new ChangeCustomer(customerId, value.Name, value.Description);
			var commandHandler = new ChangeCustomerHandler();
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

		[HttpPost("{customerId}/titles")]
		public async Task<IActionResult> RegisterTitle(string customerId, [FromBody]TitleInfo value)
		{
			var customerAggregate = await _customerRepository.GetById(customerId);
			if (customerAggregate == null || customerAggregate.IsRetired)
				return NotFound();
			var command = new RegisterTitle(customerId, value.Name, value.NumberOfSeries, value.Description);
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

		[HttpPut("{customerId}/titles/{titleId}")]
		public async Task<IActionResult> ChangeTitle(string customerId, string titleId, [FromBody]TitleInfo value)
		{
			var customerAggregate = await _customerRepository.GetById(customerId);
			if (customerAggregate == null || customerAggregate.IsRetired)
				return NotFound();
			var command = new ChangeTitle(customerId, titleId, value.Name, value.NumberOfSeries, value.Description);
			var commandHandler = new ChangeTitleHandler();
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

		[HttpDelete("{customerId}")]
		public async Task<IActionResult> RetireCustomer(string customerId)
		{
			var customerAggregate = await _customerRepository.GetById(customerId);
			if (customerAggregate == null || customerAggregate.IsRetired)
				return NotFound();
			var command = new RetireCustomer(customerId);
			var commandHandler = new RetireCustomerHandler();
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

		[HttpDelete("{customerId}/titles/{titleId}")]
		public async Task<IActionResult> RemoveTitle(string customerId, string titleId)
		{
			var customerAggregate = await _customerRepository.GetById(customerId);
			if (customerAggregate == null || customerAggregate.IsRetired)
				return NotFound();
			var command = new RemoveTitle(customerId, titleId);
			var commandHandler = new RemoveTitleHandler();
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

		[HttpGet("{customerId}")]
		public IActionResult Details(string customerId)
		{
			var customer = _queryStore
				.AsQueryable<CustomerPlain>()
				.FirstOrDefault(c => c.Id == customerId);

			if (customer == null)
				return NotFound();

			return Ok(customer);
		}

		private readonly NexterGenerator _nexter;
		private readonly IEventStore _eventStore;
		private readonly IQueryStore _queryStore;
		CustomerAggregateRepository _customerRepository;
	}
}
