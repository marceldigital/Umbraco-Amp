using System.Collections.Generic;
using System.Web;

namespace MarcelDigital.Umbraco.Amp.Providers {
    public class AmpConversionResult {
        /// <summary>
        ///     The converted HTML as AMP HTML.
        /// </summary>
        public IHtmlString AmpHtml { get; }

        /// <summary>
        ///     The AMP components that are required to be imported to the AMP page.
        /// </summary>
        public IList<string> RequiredAmpComponents { get; }

        public AmpConversionResult(IHtmlString ampHtml, IList<string> requiredAmpComponents) {
            AmpHtml = ampHtml;
            RequiredAmpComponents = requiredAmpComponents;
        }
    }
}