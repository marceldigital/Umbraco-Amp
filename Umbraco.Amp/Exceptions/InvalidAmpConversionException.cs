using System;
using HtmlAgilityPack;
using MarcelDigital.Umbraco.Amp.Converters;

namespace MarcelDigital.Umbraco.Amp.Exceptions {
    /// <summary>
    ///     Exception for when an AMP converter is used for a HTML element it was not meant for.
    /// </summary>
    public class InvalidAmpConversionException : Exception {
        /// <summary>
        ///     Constructor for the invalid AMP conversion exception.
        /// </summary>
        /// <param name="htmlNode">The HTML node that was being processed.</param>
        /// <param name="ampConverter">The converter being used to process the HTML node.</param>
        public InvalidAmpConversionException(HtmlNode htmlNode, IAmpHtmlConverter ampConverter)
            : base(
                $"Html element {htmlNode.Name} cannot be used with {ampConverter.GetType().Name}. It can only be used to convert HTML elements of type {ampConverter.HtmlTag}"
            ) {}
    }
}