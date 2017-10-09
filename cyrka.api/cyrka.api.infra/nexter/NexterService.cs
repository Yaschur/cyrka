using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace cyrka.api.infra.nexter
{
	public class NexterService
	{
		public NexterService()
		{
			_current = new Dictionary<string, long>();
			_block = new TransformBlock<(string, long), long>((Func<(string, long), long>)Next);
		}

		public Task<long> GetNextInt(string key, long input)
		{
			_block.Post((key, input));
			return _block.ReceiveAsync();
		}

		private Dictionary<string, long> _current;
		private TransformBlock<(string, long), long> _block;
		private long Next((string, long) inp)
		{
			(var key, var input) = inp;
			if (!_current.ContainsKey(key) || _current[key] < input)
				_current[key] = input;
			return ++_current[key];
		}
	}
}
