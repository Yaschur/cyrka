using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Reactive.Linq;

namespace cyrka.api.infra.nexter
{
	public class NexterService
	{
		public NexterService()
		{
			_current = new Dictionary<string, long>();
			_block = new TransformBlock<NexterItem, NexterItem>((Func<NexterItem, NexterItem>)Next);
		}

		public async Task<long> GetNextInt(string key, long input)
		{
			var inItem = new NexterItem(key, input);
			_block.Post(inItem);
			var outItem = await _block.AsObservable()
				.FirstAsync(ni => inItem.InstanceKey == ni.InstanceKey && inItem.Key == ni.Key);
			return outItem.Value;
		}

		private Dictionary<string, long> _current;
		private TransformBlock<NexterItem, NexterItem> _block;
		private NexterItem Next(NexterItem inp)
		{
			if (!_current.ContainsKey(inp.Key) || _current[inp.Key] < inp.Value)
				_current[inp.Key] = inp.Value;
			return new NexterItem(inp, ++_current[inp.Key]);
		}

		class NexterItem
		{
			public NexterItem(string key, long value)
			{
				Key = key;
				Value = value;
				InstanceKey = Guid.NewGuid().ToString();
			}
			public NexterItem(NexterItem input, long value)
			{
				Key = input.Key;
				Value = value;
				InstanceKey = input.InstanceKey;
			}
			public string Key { get; }
			public string InstanceKey { get; }
			public long Value { get; }
		}
	}
}
