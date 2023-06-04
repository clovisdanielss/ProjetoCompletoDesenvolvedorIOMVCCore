using Dev.App.ViewModels;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Dev.App.Extensions
{
    public static class RazorExtensions
    {
        public static string FormatDocument(this RazorPage page, SupplierType typePerson, string document)
        {
            return typePerson == SupplierType.PF ?
          Convert.ToUInt64(document).ToString(@"000\.000\.000\-00") :
          Convert.ToUInt64(document).ToString(@"00\.000\.000\/0000\-00");
        }
    }
}
