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
			_current = new Dictionary<string, int>();
			_block = new TransformBlock<(string, int), int>((Func<(string, int), int>)Next);
		}

		public Task<int> GetNextInt(string key, int input)
		{
			_block.Post((key, input));
			return _block.ReceiveAsync();
		}

		private Dictionary<string, int> _current;
		private TransformBlock<(string, int), int> _block;
		private int Next((string, int)inp)
		{
			(var key, var input) = inp;
			if (!_current.ContainsKey(key) || _current[key] < input)
				_current[key] = input;
			return ++_current[key];
		}
	}
}
