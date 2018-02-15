using System;
using System.Linq;
using System.Threading.Tasks;
using cyrka.api.common.events;
using cyrka.api.common.generators;
using cyrka.api.common.identities;

namespace cyrka.api.domain.projects.commands.register
{
	public class RegisterProjectHandler
	{
		const string DepartmentPrefix = "SUBT";

		public RegisterProjectHandler(IEventStore eventStore, NexterGenerator nexterGenerator)
		{
			_eventStore = eventStore;
			_nexter = nexterGenerator;
		}

		public async Task<ProjectRegistered> Handle(RegisterProject command)
		{
			var zId = CreateId(0);
			var lastSerial = (await _eventStore
				.FindLastNWithDataOf<ProjectRegistered>(10, e => e.EventData.AggregateId.StartsWith(zId.CommonPrefix)))
				.Select(e => ((CompositeSerialId)e.EventData.AggregateId).Serial)
				.OrderByDescending(s => s)
				.FirstOrDefault();
			var newSerial = await _nexter.GetNextNumber(zId.CommonPrefix, lastSerial);
			var newId = CreateId(newSerial);
			return new ProjectRegistered(newId);
		}

		private readonly IEventStore _eventStore;
		private readonly NexterGenerator _nexter;

		private CompositeSerialId CreateId(ulong serial)
		{
			var nowDate = DateTime.UtcNow;
			return new CompositeSerialId(serial, "SUBST", nowDate.ToString("yyMM"));
		}
	}
}
