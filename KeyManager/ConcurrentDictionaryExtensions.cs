using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyManager
{
	internal static class ConcurrentDictionaryExtensions
	{
		internal static async Task<TValue> GetOrAddAsync<TKey, TValue>(
			this ConcurrentDictionary<TKey, TValue> dict,
			TKey key, Func<TKey, Task<TValue>> generator)
		{
			TValue value;
			while (true)
			{
				if (dict.TryGetValue(key, out value))
				{
					return value;
				}

				value = await generator(key).ConfigureAwait(false);
				if (dict.TryAdd(key, value))
				{
					return value;
				}
			}
		}
	}
}
