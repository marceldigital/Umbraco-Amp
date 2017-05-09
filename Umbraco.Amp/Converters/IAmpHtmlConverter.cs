using HtmlAgilityPack;

namespace MarcelDigital.Umbraco.Amp.Converters {
    /// <summary>
    ///     Interface for converters of HTML to AMP HTML components.
    /// </summary>
    public interface IAmpHtmlConverter {
        /// <summary>
        ///     The HTML tag that this converter is vlaid for.
        /// </summary>
        string HtmlTag { get; }

        /// <summary>
        ///     The AMP component that the HTML tag is converted to.
        /// </summary>
        string AmpComponent { get; }

        /// <summary>
        ///     Converts the HTML node into a valid AMP HTML component.
        /// </summary>
        /// <param name="node">The HTML node to convert to an AMP Component</param>
        void Convert(HtmlNode node);
    }
}