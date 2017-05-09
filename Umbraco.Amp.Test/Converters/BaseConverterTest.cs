using System;
using HtmlAgilityPack;
using MarcelDigital.Umbraco.Amp.Converters;
using MarcelDigital.Umbraco.Amp.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MarcelDigital.Umbraco.Amp.Test.Converters {
    public abstract class BaseConverterTest {
        protected HtmlDocument HtmlDocument;
        protected IAmpHtmlConverter Converter;


        [TestInitialize]
        public void Setup() {
            HtmlDocument = new HtmlDocument();
            Converter = new AmpImgConverter();
        }

        [TestCleanup]
        public void Teardown() {
            HtmlDocument = null;
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void TestConvertNullArg() {
            Converter.Convert(null);
        }

        [TestMethod, ExpectedException(typeof(InvalidAmpConversionException))]
        public void TestConvertNoImgTag() {
            var node = HtmlDocument.CreateElement("tag");
            Converter.Convert(node);
        }
    }
}
