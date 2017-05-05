using System.Web.Mvc;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace MarcelDigital.Umbraco.Amp.Controllers
{
    public class RenderAmpMvcController : RenderMvcController {
        /// <summary>
        /// Hijacks the Amp alternate template route and routes it to
        /// the AMO view, returns 404 if no view is found.
        /// </summary>
        public virtual ActionResult Amp(RenderModel model) {
            return AmpTemplate(model);
        }

        /// <summary>
        /// Finds the AMP view for the document type in the /Views/Amp
        /// folder.
        /// </summary>
        public ActionResult AmpTemplate(RenderModel model) {
            var template = $"/Views/Amp/{model.Content.DocumentTypeAlias}.cshtml";
            // If the template does not exist go to a 404 page because there is no AMP version
            if (EnsurePhsyicalViewExists(template) == false)
                return new HttpNotFoundResult("No AMP version of this content found.");
            return View(template, model);
        }
    }
}