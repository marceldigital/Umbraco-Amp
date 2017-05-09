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

        [TestMethod]
        public void TestConvertImgTagWithDimensionsInTag() {
            const string actual = "200";
            var node = HtmlDocument.CreateElement(ImgTagName);
            node.Attributes.Add(Width, actual);
            node.Attributes.Add(Height, actual);

            Converter.Convert(node);

            Assert.AreEqual(node.Name, AmpComponentName);
            Assert.AreEqual(node.GetAttributeValue(Width, "0"), actual);
            Assert.AreEqual(node.GetAttributeValue(Height, "0"), actual);
        }

        [TestMethod]
        public void TestConvertImgWithDimensoinsInSrc() {
            var node = HtmlDocument.CreateElement(ImgTagName);
            node.Attributes.Add("src", "/website-media/3030/banner.jpg?anchor=center&mode=crop&width=730&height=380&rnd=130995077200000000");

            Converter.Convert(node);

            Assert.AreEqual(node.Name, AmpComponentName);
            Assert.AreEqual(node.GetAttributeValue(Width, "0"), "730");
            Assert.AreEqual(node.GetAttributeValue(Height, "0"), "380");
        }

        [TestMethod]
        public void TestConvertImageWithNoDimensions() {
            var parentNode = HtmlDocument.CreateElement("div");
            var node = HtmlDocument.CreateElement(ImgTagName);
            parentNode.AppendChild(node);
            node.Attributes.Add("src", "/website-media/3030/banner.jpg");

            Converter.Convert(node);

            Assert.IsNull(node.ParentNode);
        }
    }
}
