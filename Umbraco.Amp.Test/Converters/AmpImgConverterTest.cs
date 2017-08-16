using System;
using HtmlAgilityPack;
using MarcelDigital.Umbraco.Amp.Converters;
using MarcelDigital.Umbraco.Amp.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MarcelDigital.Umbraco.Amp.Test.Converters {
    [TestClass]
    public class AmpImgConverterTest : BaseConverterTest {
        private const string Width = "width";
        private const string ImgTagName = "img";
        private const string Height = "height";
        private const string AmpComponentName = "amp-img";

        [TestInitialize]
        public void Setup() {
            Scaffold(new AmpImgConverter());
        }

        [TestMethod]
        public void TestConvertImgTagWithDimensionsInTag() {
            const string numberOfPixels = "200";
            var node = HtmlDocument.CreateElement(ImgTagName);
            node.Attributes.Add(Width, numberOfPixels);
            node.Attributes.Add(Height, numberOfPixels);

            Sut.Convert(node);

            Assert.AreEqual(AmpComponentName, node.Name);
            Assert.AreEqual(numberOfPixels, node.GetAttributeValue(Width, "0"));
            Assert.AreEqual(numberOfPixels, node.GetAttributeValue(Height, "0"));
        }

        [TestMethod]
        public void TestConvertImgWithDimensionsInSrc() {
            var node = HtmlDocument.CreateElement(ImgTagName);
            node.Attributes.Add("src", "/website-media/3030/banner.jpg?anchor=center&mode=crop&width=730&height=380&rnd=130995077200000000");

            Sut.Convert(node);

            Assert.AreEqual(AmpComponentName, node.Name);
            Assert.AreEqual("730", node.GetAttributeValue(Width, "0"));
            Assert.AreEqual("380", node.GetAttributeValue(Height, "0"));
        }

        [TestMethod]
        public void TestConvertImageWithNoDimensions() {
            var parentNode = HtmlDocument.CreateElement("div");
            var node = HtmlDocument.CreateElement(ImgTagName);
            parentNode.AppendChild(node);
            node.Attributes.Add("src", "/website-media/3030/banner.jpg");

            Sut.Convert(node);

            Assert.IsNull(node.ParentNode);
        }
    }
}
