using System;
using HtmlAgilityPack;
using MarcelDigital.Umbraco.Amp.Converters;
using MarcelDigital.Umbraco.Amp.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MarcelDigital.Umbraco.Amp.Test.Converters {
    [TestClass]
    public class AmpImgConverterTest {
        private const string Width = "width";
        private const string ImgTagName = "img";
        private const string Height = "height";
        private const string AmpComponentName = "amp-img";
        private HtmlDocument _htmlDocument;
        private IAmpHtmlConverter _converter;
        

        [TestInitialize]
        public void Setup() {
            _htmlDocument = new HtmlDocument();
            _converter = new AmpImgConverter();
        }

        [TestCleanup]
        public void Teardown() {
            _htmlDocument = null;
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void TestConvertNullArg() {
            _converter.Convert(null);
        }

        [TestMethod, ExpectedException(typeof(InvalidAmpConversionException))]
        public void TestConvertNoImgTag() {
            var node = _htmlDocument.CreateElement("div");
            _converter.Convert(node);
        }

        [TestMethod]
        public void TestConvertImgTagWithDimensionsInTag() {
            const string actual = "200";
            var node = _htmlDocument.CreateElement(ImgTagName);
            node.Attributes.Add(Width, actual);
            node.Attributes.Add(Height, actual);

            _converter.Convert(node);

            Assert.AreEqual(node.Name, AmpComponentName);
            Assert.AreEqual(node.GetAttributeValue(Width, "0"), actual);
            Assert.AreEqual(node.GetAttributeValue(Height, "0"), actual);
        }

        [TestMethod]
        public void TestConvertImgWithDimensoinsInSrc() {
            var node = _htmlDocument.CreateElement(ImgTagName);
            node.Attributes.Add("src", "/website-media/3030/banner.jpg?anchor=center&mode=crop&width=730&height=380&rnd=130995077200000000");

            _converter.Convert(node);

            Assert.AreEqual(node.Name, AmpComponentName);
            Assert.AreEqual(node.GetAttributeValue(Width, "0"), "730");
            Assert.AreEqual(node.GetAttributeValue(Height, "0"), "380");
        }

        [TestMethod]
        public void TestConvertImageWithNoDimensions() {
            var parentNode = _htmlDocument.CreateElement("div");
            var node = _htmlDocument.CreateElement(ImgTagName);
            parentNode.AppendChild(node);
            node.Attributes.Add("src", "/website-media/3030/banner.jpg");

            _converter.Convert(node);

            Assert.IsNull(node.ParentNode);
        }
    }
}
