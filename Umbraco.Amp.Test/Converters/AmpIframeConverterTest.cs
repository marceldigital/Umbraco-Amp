using System;
using MarcelDigital.Umbraco.Amp.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MarcelDigital.Umbraco.Amp.Test.Converters {
    [TestClass]
    public class AmpIframeConverterTest : BaseConverterTest {
        private const string SandboxAttribuiteName = "sandbox";
        private const string IframeElementName = "iframe";
        private const string AmpComponentName = "amp-iframe";
        private const string CorrectSandboxAttributeValue = "allow-scripts allow-same-origin";

        [TestInitialize]
        public void Setup() {
            Scaffold(new AmpIframeConverter());
        }


        [TestMethod]
        public void ConvertHasCorrectSandbox() {
            var node = HtmlDocument.CreateElement(IframeElementName);
            node.Attributes.Add(SandboxAttribuiteName, CorrectSandboxAttributeValue);

            Sut.Convert(node);

            Assert.AreEqual(AmpComponentName, node.Name);
            Assert.AreEqual(CorrectSandboxAttributeValue, node.GetAttributeValue(SandboxAttribuiteName, ""));
        }

        [TestMethod]
        public void ConvertHasNoSanbox() {
            var node = HtmlDocument.CreateElement(IframeElementName);

            Sut.Convert(node);

            Assert.AreEqual(AmpComponentName, node.Name);
            Assert.AreEqual(CorrectSandboxAttributeValue, node.GetAttributeValue(SandboxAttribuiteName, ""));
        }

        [TestMethod]
        public void ConvertHasOtherSanbox() {
            var node = HtmlDocument.CreateElement(IframeElementName);
            node.Attributes.Add(SandboxAttribuiteName, "another allow-same-origin");

            Sut.Convert(node);

            Assert.AreEqual(AmpComponentName, node.Name);
            Assert.AreEqual("another allow-same-origin allow-scripts", node.GetAttributeValue(SandboxAttribuiteName, ""));
        }
    }
}
