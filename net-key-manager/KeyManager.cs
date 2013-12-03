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
		protected string _keyDir;
		protected string _app;
		protected string _privsuffix;
		protected string _pubsuffix;

		public KeyManager(string app, string env, string keyDir = null, string privsuffix = "key", string pubsuffix = "key.pub")
		{
			Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(app));
			Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(env));
			Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(privsuffix));
			Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(pubsuffix));

			_app = app;
			_privsuffix = privsuffix;
			_pubsuffix = pubsuffix;
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
				if (string.IsNullOrWhiteSpace(tcid))
				{
					return new Key(null, null);
				}
				var privPath = _app.Equals(tcid) ? Path.Combine(_keyDir, tcid, _privsuffix) : null;
				var pubPath = Path.Combine(_keyDir, tcid, _pubsuffix);
				return new Key(privPath, pubPath);
			}
		}
	}
}
