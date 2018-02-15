using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace cyrka.api.common.generators
{
	public class NexterGenerator
	{
		public NexterGenerator()
		{
			_current = new Dictionary<string, ulong>();
			_block = new TransformBlock<NexterItem, NexterItem>((Func<NexterItem, NexterItem>)Next);
		}

		public async Task<ulong> GetNextNumber(string key, ulong input)
		{
			var inItem = new NexterItem(key, input);
			var holder = new AsyncSubject<NexterItem>();
			using (
				_block.AsObservable()
					.FirstAsync(ni => inItem.InstanceKey == ni.InstanceKey && inItem.Key == ni.Key)
					.Subscribe(ni => { holder.OnNext(ni); holder.OnCompleted(); })
			)
			{
				_block.Post(inItem);
				var outItem = await holder.FirstAsync();
				return outItem.Value;
			}
		}

		private Dictionary<string, ulong> _current;
		private TransformBlock<NexterItem, NexterItem> _block;
		private NexterItem Next(NexterItem inp)
		{
			if (!_current.ContainsKey(inp.Key) || _current[inp.Key] < inp.Value)
				_current[inp.Key] = inp.Value;
			return new NexterItem(inp, ++_current[inp.Key]);
		}

		class NexterItem
		{
			public NexterItem(string key, ulong value)
			{
				Key = key;
				Value = value;
				InstanceKey = Guid.NewGuid().ToString();
			}
			public NexterItem(NexterItem input, ulong value)
			{
				Key = input.Key;
				Value = value;
				InstanceKey = input.InstanceKey;
			}
			public string Key { get; }
			public string InstanceKey { get; }
			public ulong Value { get; }
		}
	}
}
