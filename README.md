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