using System;
using System.Linq;
using System.Threading.Tasks;
using cyrka.api.common.events;
using cyrka.api.common.identities;

namespace cyrka.api.domain.projects.commands.register
{
	public class RegisterProjectHandler
	{
		const string DepartmentPrefix = "SUBT";

		public async Task<ProjectRegistered> Handle(RegisterProject command)
		{
			var zId = GetZeroId();
			var lastSerial = (await _eventStore
				.FindLastNWithDataOf<ProjectRegistered>(10, e => e.EventData.AggregateId.StartsWith(zId.CommonPrefix)))
				.Select(e => ((CompositeSerialId)e.EventData.AggregateId).Serial)
				.OrderByDescending(s => s)
				.FirstOrDefault();
			var newSerial =
		}

		private readonly IEventStore _eventStore;
		private readonly NexterGenerator _nexter;

		private CompositeSerialId GetZeroId() {
			var nowDate = DateTime.UtcNow;
			return new CompositeSerialId(0, "SUBST", nowDate.ToString("yyMM"));
		}
	}
}
