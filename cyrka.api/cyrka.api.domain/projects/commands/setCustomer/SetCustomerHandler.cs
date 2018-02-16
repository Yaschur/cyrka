using System.Threading.Tasks;

namespace cyrka.api.domain.projects.commands.setCustomer
{
	public class SetCustomerHandler
	{
		public SetCustomerHandler(ProjectAggregateRepository repository)
		{
			_repository = repository;
		}

		public async Task<CustomerSet> Handle(SetCustomer command)
		{
			var project = await _repository.GetById(command.ProjectId);
			if (project == null)
				return null;
			return new CustomerSet(project.State.ProjectId, command.CustomerId, command.CustomerName);
		}

		private readonly ProjectAggregateRepository _repository;
	}
}
