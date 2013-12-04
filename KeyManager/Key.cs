using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace KeyManager
{
	public class Key
	{
		protected string _path;
		protected string _privSuffix;
		protected string _pubSuffix;
		protected string _privPemSuffix;
		protected string _pubPemSuffix;
		protected X509Certificate2 _priv;
		protected X509Certificate2 _pub;
		protected string _privPem;
		protected string _pubPem;

		public X509Certificate2 Priv
		{
			get
			{
				if (_priv != null) return _priv;
				if (_privSuffix == null) return null;
				return _priv = LoadKey(_privSuffix);
			}
		}

		public X509Certificate2 Pub
		{
			get
			{
				if (_pub != null) return _pub;
				if (_pubSuffix == null) return null;
				return _pub = LoadKey(_pubSuffix);
			}
		}

		public string PrivPem
		{
			get
			{
				if (_privPem != null) return _privPem;
				if (_privPemSuffix == null) return null;
				return _privPem = LoadPemKey(_privPemSuffix);
			}
		}

		public string PubPem
		{
			get
			{
				if (_pubPem != null) return _pubPem;
				if (_pubPemSuffix == null) return null;
				return _pubPem = LoadPemKey(_pubPemSuffix);
			}
		}

		internal Key(string path, string privSuffix, string pubSuffix, string privPemSuffix, string pubPemSuffix)
		{
			Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(path));

			_path = path;
			_privSuffix = privSuffix;
			_pubSuffix = pubSuffix;
			_privPemSuffix = privPemSuffix;
			_pubPemSuffix = pubPemSuffix;
		}

		protected X509Certificate2 LoadKey(string suffix)
		{
			return new X509Certificate2(Path.Combine(_path, suffix));
		}

		protected string LoadPemKey(string suffix)
		{
			using (StreamReader SourceReader = File.OpenText(Path.Combine(_path, suffix)))
			{
				return SourceReader.ReadToEnd();
			}
		}
	}
}
