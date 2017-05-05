using MarcelDigital.UmbracoExtensions.StarterKit.Core.Specifications;

namespace MarcelDigital.Umbraco.Amp.Specifications.Templates {
    class AmpSpecification : ITemplateSpecification {
        public string Name => "AMP";

        public string Alias => "Amp";

        public string Content => @"
@inherits UmbracoTemplatePage
@*
    Redirects back to the canonical version
*@
@{
    Response.Redirect(Model.Content.Url);
}
";
    }
}
