using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebMoney.Cryptography.Tests
{
    [TestClass]
    public class SignerTests
    {
        [TestMethod]
        public void Test1()
        {
            const string xmlKey =
                "<RSAKeyValue><Modulus>VR//nsL+80zQAme+udfPanJjZuTJXbVid0Q8QntlnfAedr57TZnFjKSmCWJspJGU7sLs3VP1c0bs1uGw1MvW8G4F</Modulus><D>fbI2CL2ejA+o7a4CbClZGyKwzzfC/qcTsSm/C4hXwFkbvaTpsx0uzF+gj+Cd5qHlDGzum9Jn45AQzr1Y/qoou78A</D></RSAKeyValue>";

            using (var signer = new Signer())
            {
                signer.Initialize(xmlKey);

                // test
                string etalon =
                    "ba4fb77e2cb230f3746f5844df4d4bfb8b939c5a39a55cd5c4cdd0c8371cb0c564ca079f96602065aab8b784e1581db5df0a3b5cef19dda9b1827577de18077e02eb";
                string signature = signer.Sign("test", false);

                Assert.AreEqual(etalon, signature);

                // LongString
                etalon =
                    "f9c6145f8e5c79a412dbc330c872e59990b55105d0ecf97f556c662c3b878a464f81067e82174ac21a37f1bd54f0618f2fcc6f4c7a355605bc6ad3bafb01f7e20126";
                signature =
                    signer.Sign(
                        "LongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongStringLongString",
                        false);

                Assert.AreEqual(etalon, signature);

                // Sybmols
                etalon =
                    "61b8260b9d5d7cce8e1c0262f0722b8114a7b3e233d76f4b93d08933a89d23d73acfb2d960966b0c175a8604e7c71c391b7f2a64743b3c3b11c5d07ed224445300b3";
                signature =
                    signer.Sign(
                        "!@#$%^&*()-=_{}[]\"'1234567890.,%:?<>/\\",
                        false);

                Assert.AreEqual(etalon, signature);

                // Russian
                etalon =
                    "49a133e33418fe2c90d91332dd8e8e9c4fe9b09792b8e7c164ab7e3ce4a949c5855e3ed96ab62f40c6d4c7abad295f7ae01a6a58d4c984b1d44740d1000e6ce203b9";
                signature =
                    signer.Sign(
                        "ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞßàáâãäåçæèéêëìíîïðñòóôõö÷øùúûüýþÿ",
                        false);

                Assert.AreEqual(etalon, signature);

                // Blanks
                etalon =
                    "d5295349168cdb6fdbdb9c1984836cc1db939dda1dd6e18112db007154db8065b25583c71bd952f88f24f2515fbc4bb3037e5cedd5ef33cd336960b6a655537304bf";
                signature =
                    signer.Sign(
                        "                                                                  ",
                        false);

                Assert.AreEqual(etalon, signature);

                // Special Sybols
                etalon =
                    "c37c96e02f8678a13a43a3bade0ba77275da0d9869d18856daaf4b413106a9bfea74054462d851a0c5a2596dbf9c371284c61b3741b35343c91d2978db328c2f00c9";
                signature =
                    signer.Sign(
                        "\n\n\n\n\t\t\t\t\n\n\n\n",
                        false);

                Assert.AreEqual(etalon, signature);

                // Mixed
                etalon =
                   "79848351bebd6476720ae9837abaad5fbf4bb82235956179369b664aec3f097b0a513b8497371f164e5e492a884aa34283f23b0cefba3edc189745ab6bb35e78032f";
                signature =
                    signer.Sign(
                        "\n&&&<Òåêñò> !@#$%^ &*()-=_{ÀÁÂÃÄ Å ¨ Æ    Ç È É Ê Ë    Ì ÍÎ ÏÐÑÒÓ   Ô ÕÖ×ØÙÚÛÜÝÞßàáâãäå¸çæèéêëìíîïðñòóôõö÷øùúûüýþÿ}[]\"'1234567890.,%:?<><b>Ïðèâåò ìèð!</b></testing>",
                        false);

                Assert.AreEqual(etalon, signature);
            }
        }
    }
}