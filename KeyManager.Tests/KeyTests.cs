using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KeyManager.Tests
{
	[TestClass]
	public class KeyTests
	{
		[TestMethod]
		public void ReturnsPublicKeys()
		{
			var keys = new KeyManager("keytest", "keytestenv");
			Assert.IsNotNull(keys["otherapp"].Pub);
			Assert.IsFalse(string.IsNullOrWhiteSpace(keys["otherapp"].PubPem));
		}

		[TestMethod]
		public void ReturnsPrivateKeys()
		{
			var keys = new KeyManager("keytest", "keytestenv");
			Assert.IsNotNull(keys["keytest"].Priv);
			Assert.IsFalse(string.IsNullOrWhiteSpace(keys["keytest"].PrivPem));
		}

		[TestMethod]
		public void DoesNotReturnPrivateKeysForOtherApps()
		{
			var keys = new KeyManager("keytest", "keytestenv");
			Assert.IsNull(keys["otherapp"].Priv);
			Assert.IsTrue(string.IsNullOrWhiteSpace(keys["otherapp"].PrivPem));
		}
	}
}
