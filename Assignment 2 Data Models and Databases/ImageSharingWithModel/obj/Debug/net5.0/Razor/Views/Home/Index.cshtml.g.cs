#pragma checksum "C:\Users\Jakhar\Documents\Assignment2-Code\Assignment2-Code\ImageSharingWithModel\ImageSharingWithModel\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "df4c46b3a0b27e5a216b4b21f0e3ca91ee524649"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Jakhar\Documents\Assignment2-Code\Assignment2-Code\ImageSharingWithModel\ImageSharingWithModel\Views\_ViewImports.cshtml"
using ImageSharingWithModel;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Jakhar\Documents\Assignment2-Code\Assignment2-Code\ImageSharingWithModel\ImageSharingWithModel\Views\_ViewImports.cshtml"
using ImageSharingWithModel.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"df4c46b3a0b27e5a216b4b21f0e3ca91ee524649", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e06208612d3be749c3fd8c150a1d29d7629f5a99", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\Jakhar\Documents\Assignment2-Code\Assignment2-Code\ImageSharingWithModel\ImageSharingWithModel\Views\Home\Index.cshtml"
  
    ViewBag.Title = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h2>");
#nullable restore
#line 5 "C:\Users\Jakhar\Documents\Assignment2-Code\Assignment2-Code\ImageSharingWithModel\ImageSharingWithModel\Views\Home\Index.cshtml"
Write(ViewBag.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n\r\n<p>Welcome, ");
#nullable restore
#line 7 "C:\Users\Jakhar\Documents\Assignment2-Code\Assignment2-Code\ImageSharingWithModel\ImageSharingWithModel\Views\Home\Index.cshtml"
       Write(ViewBag.Username);

#line default
#line hidden
#nullable disable
            WriteLiteral("!</p>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
