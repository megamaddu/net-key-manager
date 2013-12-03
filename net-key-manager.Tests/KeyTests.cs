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
			Assert.IsFalse(string.IsNullOrWhiteSpace(keys["otherapp"].Pub.Result));
		}

		[TestMethod]
		public void ReturnsPrivateKeys()
		{
			var keys = new KeyManager("keytest", "keytestenv");
			Assert.IsFalse(string.IsNullOrWhiteSpace(keys["keytest"].Priv.Result));
		}

		[TestMethod]
		public void DoesNotReturnPrivateKeysForOtherApps()
		{
			var keys = new KeyManager("keytest", "keytestenv");
			Assert.IsTrue(string.IsNullOrWhiteSpace(keys["otherapp"].Priv.Result));
		}
	}
}
