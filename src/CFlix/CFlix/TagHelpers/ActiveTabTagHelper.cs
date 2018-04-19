using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace CFlix.TagHelpers
{
    public class ActiveTabTagHelper : AnchorTagHelper
    {
        public ActiveTabTagHelper(IHtmlGenerator generator) : base(generator)
        {
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            base.Process(context, output);

            string classes = ViewContext.RouteData.Values["controller"] as string == Controller ? " active item" : "item";
            if (output.Attributes.TryGetAttribute("class", out var tag))
            {
                output.Attributes.SetAttribute("class", tag.Value.ToString() + classes);
            }
            else
            {
                output.Attributes.SetAttribute("class", classes);
            }
        }
    }
}
