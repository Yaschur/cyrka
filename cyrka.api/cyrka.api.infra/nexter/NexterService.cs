using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace cyrka.api.infra.nexter
{
	class NexterService
	{
		public NexterService()
		{
			_current = int.MinValue;
			_block = new TransformBlock<int, int>((Func<int, int>)Next);
		}

		public Task<int> GetNextInt(int input)
		{
			_block.Post(input);
			return _block.ReceiveAsync();
		}

		private int _current;
		private TransformBlock<int, int> _block;
		private int Next(int input)
		{
			if (input > _current)
				_current = input;
			return ++_current;
		}
	}
}
