using System;
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
using cyrka.api.common.generators;
using cyrka.api.web.models.customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using cyrka.api.web.services;

namespace cyrka.api.web.controllers
{
	[Route("customers"), Authorize]
	public class CustomersController : Controller
	{
		const string EventChannelKey = "events";

		public CustomersController(IQueryStore queryStore)
		{
			_queryStore = queryStore;
		}

		[HttpPost]
		public async Task<IActionResult> RegisterCustomer(
			[FromBody] CustomerInfo value,
			[FromServices] CustomerCommandService<RegisterCustomer> customerCommandService
		)
		{
			var command = new RegisterCustomer(value.Name, value.Description);
			var result = await customerCommandService.Do(command);
			return result;
		}

		[HttpPut("{customerId}")]
		public async Task<IActionResult> ChangeCustomer(
			string customerId,
			[FromBody] CustomerInfo value,
			[FromServices] CustomerCommandService<ChangeCustomer> customerCommandService
		)
		{
			var command = new ChangeCustomer(value.Name, value.Description);
			var result = await customerCommandService.Do(command, customerId);
			return result;
		}

		[HttpPost("{customerId}/titles")]
		public async Task<IActionResult> RegisterTitle(
			string customerId,
			[FromBody] TitleInfo value,
			[FromServices] CustomerCommandService<RegisterTitle> customerCommandService
		)
		{
			var command = new RegisterTitle(value.Name, value.NumberOfSeries, value.Description);
			var result = await customerCommandService.Do(command, customerId);
			return result;
		}

		[HttpPut("{customerId}/titles/{titleId}")]
		public async Task<IActionResult> ChangeTitle(
			string customerId,
			string titleId,
			[FromBody] TitleInfo value,
			[FromServices] CustomerCommandService<ChangeTitle> customerCommandService
		)
		{
			var command = new ChangeTitle(titleId, value.Name, value.NumberOfSeries, value.Description);
			var result = await customerCommandService.Do(command, customerId);
			return result;
		}

		[HttpDelete("{customerId}")]
		public async Task<IActionResult> RetireCustomer(
			string customerId,
			[FromServices] CustomerCommandService<RetireCustomer> customerCommandService
		)
		{
			var command = new RetireCustomer();
			var result = await customerCommandService.Do(command, customerId);
			return result;
		}

		[HttpDelete("{customerId}/titles/{titleId}")]
		public async Task<IActionResult> RemoveTitle(
			string customerId,
			string titleId,
			[FromServices] CustomerCommandService<RemoveTitle> customerCommandService
		)
		{
			var command = new RemoveTitle(titleId);
			var result = await customerCommandService.Do(command, customerId);
			return result;
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

		private readonly IQueryStore _queryStore;
	}
}
