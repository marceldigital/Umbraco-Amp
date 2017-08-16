using System;
using MarcelDigital.Umbraco.Amp.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MarcelDigital.Umbraco.Amp.Test.Converters {
    [TestClass]
    public class AmpIframeConverterTest : BaseConverterTest {
        private const string SandboxAttributeName = "sandbox";
        private const string IframeElementName = "iframe";
        private const string AmpComponentName = "amp-iframe";

        [TestInitialize]
        public void Setup() {
            Scaffold(new AmpIframeConverter());
        }

        [TestMethod]
        public void MissingRequiredSandboxAttribute() {
            var node = HtmlDocument.CreateElement(IframeElementName);

            Sut.Convert(node);

            Assert.AreEqual(AmpComponentName, node.Name);
            Assert.IsTrue(node.HasAttributes);
            Assert.AreEqual("", node.GetAttributeValue( SandboxAttributeName, "missing"));
        }

        [TestMethod]
        public void RequiredSandboxAttribute() {
            var node = HtmlDocument.CreateElement(IframeElementName);
            node.Attributes.Add(SandboxAttributeName, "allow-scripts allow-same-origin");
            Sut.Convert(node);

            Assert.AreEqual(AmpComponentName, node.Name);
            Assert.AreEqual("allow-scripts allow-same-origin", node.GetAttributeValue(SandboxAttributeName, "missing"));
        }
    }
}
