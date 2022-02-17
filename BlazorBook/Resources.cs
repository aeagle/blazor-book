﻿namespace BlazorBook
{
    public class Resources : Microsoft.AspNetCore.Components.ComponentBase
    {
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "style");
            builder.AddContent(1, CoreUtils.GetEmbeddedWebResource("styles.css"));
            builder.CloseElement();
        }
    }
}
