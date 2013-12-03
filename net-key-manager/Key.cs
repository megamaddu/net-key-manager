using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyManager
{
	public class Key
	{
		protected static ConcurrentDictionary<string, string> __keyCache = new ConcurrentDictionary<string, string>();

		protected string _privPath;
		protected string _pubPath;

		public Task<string> Priv
		{
			get
			{
				return LoadKey(_privPath);
			}
		}

		public Task<string> Pub
		{
			get
			{
				return LoadKey(_pubPath);
			}
		}

		internal Key(string privPath, string pubPath)
		{
			_privPath = privPath;
			_pubPath = pubPath;
		}

		protected Task<string> LoadKey(string path)
		{
			return path == null ?
				Task.FromResult<string>(null)
				: __keyCache.GetOrAddAsync(path, async k =>
					{
						using (StreamReader SourceReader = File.OpenText(k))
						{
							return await SourceReader.ReadToEndAsync().ConfigureAwait(false);
						}
					});
		}
	}
}
