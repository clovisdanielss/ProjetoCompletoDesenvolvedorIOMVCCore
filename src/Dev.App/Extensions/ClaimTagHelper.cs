using Dev.App.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Dev.App.Extensions
{
    [HtmlTargetElement("*", Attributes = "suppress-by-claim-name")]
    [HtmlTargetElement("*", Attributes = "suppress-by-claim-value")]
    public class SuppressElementByClaimTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public SuppressElementByClaimTagHelper(IHttpContextAccessor context)
        {
            _contextAccessor = context;
        }

        [HtmlAttributeName("suppress-by-claim-name")]
        public string ClaimName { get; set; }
        [HtmlAttributeName("suppress-by-claim-value")]
        public string ClaimValue { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null) throw new ArgumentNullException();
            if (output == null) throw new ArgumentNullException();
            var hasAccess = CustomAuthorization.ValidateUserClaims(_contextAccessor.HttpContext, ClaimName, ClaimValue);

            if (hasAccess) return;

            output.SuppressOutput();
        }
    }

    [HtmlTargetElement("a", Attributes = "disable-by-claim-name")]
    [HtmlTargetElement("a", Attributes = "disable-by-claim-value")]
    public class DisableLinkByClaimTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public DisableLinkByClaimTagHelper(IHttpContextAccessor context)
        {
            _contextAccessor = context;
        }

        [HtmlAttributeName("disable-by-claim-name")]
        public string ClaimName { get; set; }
        [HtmlAttributeName("disable-by-claim-value")]
        public string ClaimValue { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null) throw new ArgumentNullException();
            if (output == null) throw new ArgumentNullException();
            var hasAccess = CustomAuthorization.ValidateUserClaims(_contextAccessor.HttpContext, ClaimName, ClaimValue);

            if (hasAccess) return;

            output.Attributes.RemoveAll("href");
            output.Attributes.Add(new TagHelperAttribute("style", "cursor: not-allowed"));
            output.Attributes.Add(new TagHelperAttribute("title", "você não tem permissão"));
        }
    }

    // Esse a seguir serve para caso eu não esteja no contexto de uma determinada ação;
    [HtmlTargetElement("a", Attributes = "suppress-by-action")]
    public class SuppressByActionTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public SuppressByActionTagHelper(IHttpContextAccessor context)
        {
            _contextAccessor = context;
        }

        [HtmlAttributeName("suppress-by-action")]
        public string ActionName { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null) throw new ArgumentNullException();
            if (output == null) throw new ArgumentNullException();


            var action = _contextAccessor.HttpContext.GetRouteData().Values["action"].ToString();

            if (ActionName.Contains(action)) return;

            output.SuppressOutput();
        }
    }
}
