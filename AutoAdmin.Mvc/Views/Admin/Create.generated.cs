﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASP
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    
    #line 3 "..\..\Views\Admin\Create.cshtml"
    using AutoAdmin.Mvc.Extensions;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Admin/Create.cshtml")]
    public partial class _Views_Admin_Create_cshtml : System.Web.Mvc.WebViewPage<object>
    {
        public _Views_Admin_Create_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 4 "..\..\Views\Admin\Create.cshtml"
  
    ViewBag.Title = "Create";
    Layout = "~/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n\r\n");

            
            #line 10 "..\..\Views\Admin\Create.cshtml"
 using (Html.BeginForm())
{
    
            
            #line default
            #line hidden
            
            #line 12 "..\..\Views\Admin\Create.cshtml"
Write(Html.AntiForgeryToken());

            
            #line default
            #line hidden
            
            #line 12 "..\..\Views\Admin\Create.cshtml"
                            


            
            #line default
            #line hidden
WriteLiteral("<div");

WriteLiteral(" class=\"form-horizontal\"");

WriteLiteral(">\r\n    <h4>");

            
            #line 15 "..\..\Views\Admin\Create.cshtml"
   Write(Url.RequestContext.RouteData.GetRequiredString("table"));

            
            #line default
            #line hidden
WriteLiteral("</h4>\r\n    <hr />\r\n");

            
            #line 17 "..\..\Views\Admin\Create.cshtml"
    
            
            #line default
            #line hidden
            
            #line 17 "..\..\Views\Admin\Create.cshtml"
     foreach (var property in Model.GetType().GetProperties())
    {

            
            #line default
            #line hidden
WriteLiteral("        <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n");

WriteLiteral("            ");

            
            #line 20 "..\..\Views\Admin\Create.cshtml"
       Write(Html.AutoLabelFor(property, htmlAttributes: new { @class = "control-label col-md-2" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            <div");

WriteLiteral(" class=\"col-md-10\"");

WriteLiteral(">\r\n                ");

WriteLiteral("\r\n");

WriteLiteral("                ");

            
            #line 23 "..\..\Views\Admin\Create.cshtml"
           Write(Html.AutoEditorFor(property,true));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                ");

            
            #line 24 "..\..\Views\Admin\Create.cshtml"
           Write(Html.AutoValidationMessageFor(property, new { @class = "text-danger" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </div>\r\n        </div>\r\n");

            
            #line 27 "..\..\Views\Admin\Create.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("\r\n    <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"col-md-offset-2 col-md-10\"");

WriteLiteral(">\r\n            <input");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" value=\"Create\"");

WriteLiteral(" class=\"btn btn-default\"");

WriteLiteral(" />\r\n        </div>\r\n    </div>\r\n</div>\r\n");

            
            #line 35 "..\..\Views\Admin\Create.cshtml"
}

            
            #line default
            #line hidden
WriteLiteral("\r\n<div>\r\n");

WriteLiteral("    ");

            
            #line 38 "..\..\Views\Admin\Create.cshtml"
Write(Html.ActionLink("Back to List", "Index"));

            
            #line default
            #line hidden
WriteLiteral("\r\n</div>\r\n");

        }
    }
}
#pragma warning restore 1591
