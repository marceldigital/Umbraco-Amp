using System;
using System.Linq;
using HtmlAgilityPack;
using MarcelDigital.Umbraco.Amp.Exceptions;

namespace MarcelDigital.Umbraco.Amp.Converters {
    internal class AmpIframeConverter : IAmpHtmlConverter {
        private static readonly string[] RequiredSandboxValueValues = {
            "allow-scripts",
            "allow-same-origin"
        };

        private const string SandboxAttributeName = "sandbox";

        public string HtmlTag => "iframe";
        public string AmpComponent => "amp-iframe";

        /// <summary>
        ///     Converts an HTML iframe element into a amp-iframe component.
        /// </summary>
        /// <param name="node">The HTML node to convert to an amp-iframe.</param>
        public void Convert(HtmlNode node) {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (!node.Name.ToLowerInvariant().Equals(HtmlTag)) throw new InvalidAmpConversionException(node, this);

            node.Name = AmpComponent;

            if (HasCorrectSandboxValue(node)) return;
            // It's missing the required sandbox value, add it
            SetRequiredSandboxValue(node);
        }

        /// <summary>
        ///     Checks to see if the node already has the correct sandbox value for an AMP iframe
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static bool HasCorrectSandboxValue(HtmlNode node) {
            var sandboxValueTokens = SplitSandboxValueIntoValues(node);
            return sandboxValueTokens.Intersect(RequiredSandboxValueValues).Count() == RequiredSandboxValueValues.Length;
        }

        /// <summary>
        ///     Sets the required value for sandbox on the node.
        /// </summary>
        /// <param name="node">The node to set the sandbox value for.</param>
        private static void SetRequiredSandboxValue(HtmlNode node) {
            var currentSandboxValues = SplitSandboxValueIntoValues(node).ToList();
            foreach (var requiredSandboxValueValue in RequiredSandboxValueValues) {
                if (!currentSandboxValues.Contains(requiredSandboxValueValue)) {
                    currentSandboxValues.Add(requiredSandboxValueValue);
                }
            }
            node.Attributes.Add(SandboxAttributeName, string.Join(" ", currentSandboxValues));
        }

        /// <summary>
        ///     Splits the value of an attribute by spaces.
        /// </summary>
        /// <param name="attributeValue"></param>
        /// <returns></returns>
        private static string[] SplitSandboxValueIntoValues(HtmlNode node)
            =>
                node.GetAttributeValue(SandboxAttributeName, "")
                    .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
    }
}