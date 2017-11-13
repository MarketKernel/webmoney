using System;
using System.Text;
using System.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebMoney.Cryptography.Tests
{
    [TestClass]
    public class DecryptedKeyTests
    {
        private static readonly Encoding Encoding = Encoding.GetEncoding("windows-1251");

        [TestMethod]
        public void EncryptDecrypt()
        {
            const string xmlKey =
                "<RSAKeyValue><Modulus>VR//nsL+80zQAme+udfPanJjZuTJXbVid0Q8QntlnfAedr57TZnFjKSmCWJspJGU7sLs3VP1c0bs1uGw1MvW8G4F</Modulus><D>fbI2CL2ejA+o7a4CbClZGyKwzzfC/qcTsSm/C4hXwFkbvaTpsx0uzF+gj+Cd5qHlDGzum9Jn45AQzr1Y/qoou78A</D></RSAKeyValue>";

            const long wmId = 123456789012;

            string etalon;
            byte[] encrypted;

            using (var keeperKey = new DecryptedKey(xmlKey))
            {
                var signer = new Signer();
                signer.Initialize(keeperKey);

                etalon = signer.Sign("test", false);

                using (var secureString = new SecureString())
                {
                    secureString.AppendChar('П');
                    secureString.AppendChar('а');
                    secureString.AppendChar('р');
                    secureString.AppendChar('о');
                    secureString.AppendChar('л');
                    secureString.AppendChar('ь');
                    secureString.AppendChar('1');
                    secureString.AppendChar('2');

                    secureString.MakeReadOnly();

                    encrypted = keeperKey.Encrypt(wmId, secureString);
                }
            }

            using (KeeperKey decryptedKey = DecryptedKey.Decrypt(encrypted, wmId, Encoding.GetBytes("Пароль12")))
            {
                var signer = new Signer();
                signer.Initialize(decryptedKey);
                string signature = signer.Sign("test", false);

                Assert.AreEqual(etalon, signature);

                string decryptedXmlKey = decryptedKey.ToXmlString();

                Assert.AreEqual(xmlKey, decryptedXmlKey);
            }
        }

        [TestMethod]
        public void DecryptEncrypt()
        {
            byte[] encrypted1 =
                Convert.FromBase64String(
                    "gQABAFpIWLAScLK7Pc84Nd6zZfCMAAAAAAAAAEIAqnk1hCgxMRM9srF/GjDIZfV7zLtXURoPJHagdv5OUSfMdqdlJrKT0Mr/kJ3r/zCb26ftF0fIXoyFkaIliLO5xWjLQYzAsEKCV6HsMaYb9sBuHMzm58zb+FwCqh8BXa08rK6efIvZA2fYxtrx0r+YHLtvkhh7bVHBxqpsO5rPcM4DANV8+6o=");
            const string etalone = "ba4fb77e2cb230f3746f5844df4d4bfb8b939c5a39a55cd5c4cdd0c8371cb0c564ca079f96602065aab8b784e1581db5df0a3b5cef19dda9b1827577de18077e02eb";

            using (var secureString1 = new SecureString())
            {
                secureString1.AppendChar('П');
                secureString1.AppendChar('а');
                secureString1.AppendChar('р');
                secureString1.AppendChar('о');
                secureString1.AppendChar('л');
                secureString1.AppendChar('ь');
                secureString1.AppendChar('1');
                secureString1.AppendChar('2');
                secureString1.MakeReadOnly();

                using (var keeperKey1 = DecryptedKey.Decrypt(encrypted1, 0, secureString1))
                {
                    var signer1 = new Signer();

                    signer1.Initialize(keeperKey1);

                    string signature1 = signer1.Sign("test", false);
                    Assert.AreEqual(signature1, etalone);

                    using (var secureString2 = new SecureString())
                    {
                        secureString2.AppendChar('П');
                        secureString2.AppendChar('а');
                        secureString2.AppendChar('р');

                        byte[] encrypted2 = keeperKey1.Encrypt(999999999999, secureString2, false);

                        using (var keeperKey2 = DecryptedKey.Decrypt(encrypted2, 999999999999, secureString2))
                        {
                            var signer2 = new Signer();

                            signer2.Initialize(keeperKey2);

                            string signature2 = signer2.Sign("test", false);
                            Assert.AreEqual(signature2, etalone);
                        }
                    }
                }
            }
        }

        //[TestMethod]
        //public void DecryptEncrypt2()
        //{
        //    byte[] encrypted1 = Convert.FromBase64String("");

        //    const string etalone = "";

        //    using (var keeperKey1 = DecryptedKey.Decrypt(encrypted1, 0, Encoding.UTF8.GetBytes("password12")))
        //    {
        //        var signer1 = new Signer();

        //        signer1.Initialize(keeperKey1);

        //        string signature1 = signer1.Sign("test", false);
        //        Assert.AreEqual(signature1, etalone);

        //        byte[] encrypted2 = keeperKey1.Encrypt(227583964705, Encoding.UTF8.GetBytes("password12"), false);

        //        Assert.IsTrue(encrypted1.SequenceEqual(encrypted2));

        //        using (var keeperKey2 = DecryptedKey.Decrypt(encrypted2, 227583964705, Encoding.UTF8.GetBytes("password12")))
        //        {
        //            var signer2 = new Signer();

        //            signer2.Initialize(keeperKey2);

        //            string signature2 = signer2.Sign("test", false);
        //            Assert.AreEqual(signature2, etalone);
        //        }
        //    }
        //}
    }
}