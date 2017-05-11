using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using MarcelDigital.Umbraco.Amp.Converters;

namespace MarcelDigital.Umbraco.Amp.Providers {
    public static class AmpConversionProvider {
        private static IDictionary<string, IAmpHtmlConverter> _currentConverters;

        /// <summary>
        ///     The current amp converters for the provider to use.
        /// </summary>
        private static IDictionary<string, IAmpHtmlConverter> CurrentConverters
            => _currentConverters ?? (_currentConverters = Initialize());

        /// <summary>
        ///     Adds an amp converter to the provider.
        /// </summary>
        /// <param name="converter">The conveter to add to the provider.</param>
        public static void Add(IAmpHtmlConverter converter) {
            CurrentConverters.Add(converter.HtmlTag, converter);
        }

        /// <summary>
        ///     Removes a converter from the provider.
        /// </summary>
        /// <param name="converter">The converter to remove from the provider.</param>
        public static void Remove(IAmpHtmlConverter converter) {
            CurrentConverters.Remove(converter.HtmlTag);
        }

        /// <summary>
        ///     Clears all of the converters from the provider.
        /// </summary>
        public static void Clear() {
            CurrentConverters.Clear();
        }

        /// <summary>
        ///     Converts the HTML passed in to valid AMP HTML.
        /// </summary>
        /// <param name="html">The HTML to be converted to its AMP HTML equivilant.</param>
        /// <returns></returns>
        public static AmpConversionResult Convert(IHtmlString html) {
            var requiredAmpComponents = new HashSet<string>();

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html.ToString());

            htmlDocument.DocumentNode.Descendants()
                        .ToList()
                        .ForEach(htmlNode => ProcessHtmlNode(htmlNode, requiredAmpComponents));

            return new AmpConversionResult(new HtmlString(htmlDocument.DocumentNode.OuterHtml),
                requiredAmpComponents.ToList());
        }

        /// <summary>
        ///     Processes the HTML node by converting the node to an AMP component if nessesary and stripping the style attribute
        ///     if present.
        /// </summary>
        /// <param name="htmlNode">The HTML node to convert</param>
        /// <param name="requiredAmpComponents">The list of required components to add to.</param>
        private static void ProcessHtmlNode(HtmlNode htmlNode, ISet<string> requiredAmpComponents) {
            RemoveAttribute(htmlNode, "style");
            if (!CurrentConverters.ContainsKey(htmlNode.Name)) return;
            // We got a live one! Convert it!
            var converter = CurrentConverters[htmlNode.Name];
            converter.Convert(htmlNode);
            // Add the AMP component to the required list for the result
            requiredAmpComponents.Add(converter.AmpComponent);
        }

        /// <summary>
        ///     Removes the attribute from the HTML node.
        /// </summary>
        /// <param name="node">The node to remove the attribute from.</param>
        /// <param name="attributeName">The name of the attribute to remove.</param>
        internal static void RemoveAttribute(HtmlNode node, string attributeName) {
            if (node.Attributes.Contains(attributeName)) {
                node.Attributes.Remove(attributeName);
            }
        }

        private static IDictionary<string, IAmpHtmlConverter> Initialize() {
            var imgConverter = new AmpImgConverter();
            var iframeConverter = new AmpIframeConverter();

            var converters = new Dictionary<string, IAmpHtmlConverter> {
                {imgConverter.HtmlTag, imgConverter},
                {iframeConverter.HtmlTag, iframeConverter}
            };

            return converters;
        }
    }
}