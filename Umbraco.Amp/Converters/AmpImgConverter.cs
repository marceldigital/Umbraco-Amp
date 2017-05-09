using System;
using System.Web;
using HtmlAgilityPack;
using MarcelDigital.Umbraco.Amp.Exceptions;

namespace MarcelDigital.Umbraco.Amp.Converters {
    internal class AmpImgConverter : IAmpHtmlConverter {
        private const string WidthAttributeName = "width";
        private const string HeightAttributeName = "height";
        public string HtmlTag => "img";
        public string AmpComponent => "amp-img";

        /// <summary>
        ///     Converts an img HTML tag to an AMP img component. If the required height and width tags are absent, it will try to
        ///     recover by getting it from query string parameters in the img tag src value. If it cannot make the img a valid amp
        ///     take, the image is removed.
        /// </summary>
        /// <param name="node">The img node to convert to an amp-img</param>
        public void Convert(HtmlNode node) {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (!node.Name.ToLowerInvariant().Equals(HtmlTag)) throw new InvalidAmpConversionException(node, this);

            node.Name = AmpComponent;

            if (TagHasDimensions(node)) return; // No more need to process
            if (TryConvertyWithQueryString(node)) return; // Converted using the query string

            // Failed to convert the image, remove it from the document so that we have a valid AMP page
            node.Remove();
        }

        /// <summary>
        ///     Checks if the node has the required width and height attributes.
        /// </summary>
        /// <param name="node">The HTML node to check the attributes for.</param>
        /// <returns></returns>
        private static bool TagHasDimensions(HtmlNode node) {
            return node.Attributes.Contains(WidthAttributeName) && node.Attributes.Contains(HeightAttributeName);
        }

        /// <summary>
        ///     Adds the width and height attributes to the HTML node based on the passed parameters.
        /// </summary>
        /// <param name="node">The HTML node to add the attributes to.</param>
        /// <param name="width">The width of the image.</param>
        /// <param name="height">The height of the image.</param>
        private static void AddDimensionsToTag(HtmlNode node, string width, string height) {
            node.Attributes.Add(WidthAttributeName, width);
            node.Attributes.Add(HeightAttributeName, height);
        }

        /// <summary>
        ///     Tries to add the height and width attributes to the HTML node based on the query string in the source.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static bool TryConvertyWithQueryString(HtmlNode node) {
            var srcAttributeValue = node.GetAttributeValue("src", "/");
            var parsedQueryString = HttpUtility.ParseQueryString(srcAttributeValue);

            var widthAttributeValue = parsedQueryString[WidthAttributeName];
            var heightAttributeValue = parsedQueryString[HeightAttributeName];

            if (widthAttributeValue == null || heightAttributeValue == null) return false;

            AddDimensionsToTag(node, widthAttributeValue, heightAttributeValue);
            return true;
        }
    }
}