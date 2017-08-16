using System;
using System.Web;
using HtmlAgilityPack;
using MarcelDigital.Umbraco.Amp.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MarcelDigital.Umbraco.Amp.Test.Providers {
    [TestClass]
    public class AmpConvertionProviderTest {
        private const string StyleAttributeName = "style";
        private HtmlDocument _htmlDocument;
        private HtmlNode _divNode;

        [TestInitialize]
        public void Setup() {
            _htmlDocument = new HtmlDocument();
            _divNode = _htmlDocument.CreateElement("div");
        }

        [TestCleanup]
        public void Teardown() {
            _htmlDocument = null;
        }

        [TestMethod]
        public void TestStripTag() {
            _divNode.Attributes.Add(StyleAttributeName, "border-bottom: 20px;");

            AmpConversionProvider.RemoveAttribute(_divNode, StyleAttributeName);

            Assert.AreEqual("missing", _divNode.GetAttributeValue(StyleAttributeName, "missing"));
        }

        [TestMethod]
        public void TestConversion() {
            var img = _htmlDocument.CreateElement("img");
            _divNode.Attributes.Add(StyleAttributeName, "border-bottom: 20px;");
            img.Attributes.Add("src", "/path/to/image.png");
            img.Attributes.Add("width", "200");
            img.Attributes.Add("height", "200");

            var iframe = _htmlDocument.CreateElement("iframe");
            iframe.Attributes.Add("src", "/path/to/thing");

            _divNode.ChildNodes.Add(img);
            _divNode.ChildNodes.Add(iframe);

            const string expectedText =
                "<div><amp-img src=\"/path/to/image.png\" width=\"200\" height=\"200\"></amp-img><amp-iframe src=\"/path/to/thing\" sandbox=\"\"></amp-iframe></div>";

            var result = AmpConversionProvider.Convert(new HtmlString(_divNode.OuterHtml));

            Assert.AreEqual(expectedText, result.AmpHtml.ToHtmlString());
            Assert.AreEqual(2, result.RequiredAmpComponents.Count);
            Assert.IsTrue(result.RequiredAmpComponents.Contains("amp-img"));
            Assert.IsTrue(result.RequiredAmpComponents.Contains("amp-iframe"));
        }
    }
}
