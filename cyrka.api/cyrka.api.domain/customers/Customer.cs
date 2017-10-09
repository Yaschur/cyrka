using System;

namespace cyrka.api.domain.customers
{
	public class Customer
	{
		public void Register(string id, string name, string description)
		{
			if (_customerDto != null)
				throw new InvalidOperationException("Customer is already registered");

			_customerDto = new CustomerDto
			{
				Id = id,
				Name = name,
				Description = description
			};

			// TODO: publish CustomerRegistered
		}

		private CustomerDto _customerDto { get; set; }
	}
}