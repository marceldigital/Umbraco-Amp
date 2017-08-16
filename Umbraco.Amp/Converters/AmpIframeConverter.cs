using System;
using System.Linq;
using HtmlAgilityPack;
using MarcelDigital.Umbraco.Amp.Exceptions;

namespace MarcelDigital.Umbraco.Amp.Converters {
    internal class AmpIframeConverter : IAmpHtmlConverter {
        private const string SandboxAttributeName = "sandbox";

        public string HtmlTag => "iframe";
        public string AmpComponent => "amp-iframe";

        /// <summary>
        ///     Converts an HTML iframe element into a amp-iframe component. If the required sandbox attrbute is not present on the
        ///     iframe tag it will be added with an empty value.
        /// </summary>
        /// <param name="node">The HTML node to convert to an amp-iframe.</param>
        public void Convert(HtmlNode node) {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (!node.Name.ToLowerInvariant().Equals(HtmlTag)) throw new InvalidAmpConversionException(node, this);

            node.Name = AmpComponent;

            AddSandoxAttributeIfMissing(node);
        }

        /// <summary>
        ///     Adds the requred sandbox attribute if it is missing.
        /// </summary>
        /// <param name="node">The node to add the attribute to.</param>
        private static void AddSandoxAttributeIfMissing(HtmlNode node) {
            if (!node.Attributes.Contains(SandboxAttributeName)) {
                node.Attributes.Add(SandboxAttributeName, "");
            }
        }
    }
}