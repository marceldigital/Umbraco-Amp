using System;
using HtmlAgilityPack;
using MarcelDigital.Umbraco.Amp.Converters;
using MarcelDigital.Umbraco.Amp.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MarcelDigital.Umbraco.Amp.Test.Converters {
    public abstract class BaseConverterTest {
        protected HtmlDocument HtmlDocument;
        protected IAmpHtmlConverter Sut;

        public void Scaffold(IAmpHtmlConverter sut) {
            HtmlDocument = new HtmlDocument();
            Sut = sut;
        }

        [TestCleanup]
        public void Teardown() {
            HtmlDocument = null;
            Sut = null;
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void TestConvertNullArg() {
            Sut.Convert(null);
        }

        [TestMethod, ExpectedException(typeof(InvalidAmpConversionException))]
        public void TestConvertNotRightTag() {
            var node = HtmlDocument.CreateElement("tag");
            Sut.Convert(node);
        }
    }
}
