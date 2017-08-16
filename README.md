Umbraco AMP
=

Umbraco extension create Google AMP pages for content.
      
Provides an AMP layout boilerplate (/Views/Shared/_LayoutAmp.cshtml) to base AMP pages off of.

Creates an Amp template in Umbraco so that you can start creating amp pages off of the alternate
template url in order to have Amp and traditional pages side by side. (Ex: something.org/my-content/amp)

Use the custom controller RenderAmpMvcController to unlock multiple templates off of the alternate template route.
Put AMP views in /Views/Amp and name them off of the document type alias which you want to create an AMP view for.

To make it the default controller, add this to the OnApplicationStarting method on a class implementing IApplicationEventHandler:
DefaultRenderMvcControllerResolver.Current.SetDefaultControllerType(typeof(RenderAmpMvcController));

See the Umbraco AMP exmaple [here](https://github.com/marceldigital/Umbraco-AMP-Demo)

## Converting HTML into AMP HTML

In some cases you will not have control over the HTML being displayed to the page like an RTE from the back office. For these cases this package provides a converter which will try to convert the invalid HTML elements into valid AMP HTML elements. Currently, the items that are converted are:
- __img__ to __amp-img__
- __iframe__ to __amp-iframe__

In order to use the converter, call the static method `Convert(...)` on the class `AmpConversionProvider`. This will return a `AmpConversionResult` which contains the names of the tags that were converted (so that you can add the nessesary AMP components to the page) and the converted AMP HTML.

```csharp
var originalHtml = new HtmlString(Model.Content.GetPropertyValue<string>("body"));
var result = AmpConversionProvider.Convert(originalHtml);
var ampHtml = result.AmpHtml;
```

Using a section in your AMP Layout file, you can add the required AMP components based on the conversion that occured in your view.

```csharp
@section TagImports {
    @if (result.RequiredAmpComponents.Contains("amp-iframe")) {
        <script async custom-element="amp-iframe" src="https://cdn.ampproject.org/v0/amp-iframe-0.1.js"></script>
    }
}
```