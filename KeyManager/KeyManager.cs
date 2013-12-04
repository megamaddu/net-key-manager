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
	public class KeyManager
	{
		protected ConcurrentDictionary<string, Key> _keyCache = new ConcurrentDictionary<string, Key>();
		protected string _keyDir;
		protected string _app;
		protected string _privSuffix;
		protected string _pubSuffix;
		protected string _pemPrivSuffix;
		protected string _pemPubSuffix;

		public KeyManager(string app, string env, string keyDir = null, string privSuffix = "key.crt", string pubSuffix = "key.cer", string pemPrivSuffix = "key", string pemPubSuffix = "key.pub")
		{
			Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(app));
			Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(env));
			Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(privSuffix));
			Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(pubSuffix));

			_app = app;
			_privSuffix = privSuffix;
			_pubSuffix = pubSuffix;
			_pemPrivSuffix = pemPrivSuffix;
			_pemPubSuffix = pemPubSuffix;
			if (_keyDir == null)
			{
				_keyDir = Path.Combine(keyDir ?? Environment.GetEnvironmentVariable("UserProfileKeyDir") ?? Path.Combine(Environment.GetEnvironmentVariable("UserProfile"), ".keys"), env);
			}

			Contract.Assert(Directory.Exists(_keyDir));
		}

		public Key this[string tcid]
		{
			get
			{
				return Get(tcid);
			}
		}

		public Key Get(string tcid)
		{
			if (string.IsNullOrWhiteSpace(tcid))
			{
				return null;
			}
			return _keyCache.GetOrAdd(tcid, k =>
			{
				var path = Path.Combine(_keyDir, k);
				var isPrivate = _app.Equals(k);
				return new Key(path, !isPrivate ? null : _privSuffix, _pubSuffix, !isPrivate ? null : _pemPrivSuffix, _pemPubSuffix);
			});
		}
	}
}
