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
    
    #line 2 "..\..\Views\Admin\Details.cshtml"
    using System.Collections;
    
    #line default
    #line hidden
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
    
    #line 3 "..\..\Views\Admin\Details.cshtml"
    using AutoAdmin.Mvc.Extensions;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Admin/Details.cshtml")]
    public partial class _Views_Admin_Details_cshtml : System.Web.Mvc.WebViewPage<object>
    {
        public _Views_Admin_Details_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 4 "..\..\Views\Admin\Details.cshtml"
  
    ViewBag.Title = "Details";
    Layout = "~/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<h2>Details</h2>\r\n\r\n<div>\r\n    <h4>Products</h4>\r\n    <hr />\r\n    <dl");

WriteLiteral(" class=\"dl-horizontal\"");

WriteLiteral(">\r\n\r\n");

            
            #line 16 "..\..\Views\Admin\Details.cshtml"
        
            
            #line default
            #line hidden
            
            #line 16 "..\..\Views\Admin\Details.cshtml"
         foreach (var property in Model.GetType().GetProperties())
        {

            
            #line default
            #line hidden
WriteLiteral("            <dt>\r\n");

WriteLiteral("                ");

            
            #line 19 "..\..\Views\Admin\Details.cshtml"
           Write(Html.AutoLabelFor(property));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </dt>\r\n");

WriteLiteral("            <dd>\r\n");

WriteLiteral("                ");

            
            #line 22 "..\..\Views\Admin\Details.cshtml"
           Write(Html.AutoDisplayFor(property));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </dd>\r\n");

            
            #line 24 "..\..\Views\Admin\Details.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("    </dl>\r\n</div>\r\n<p>\r\n");

WriteLiteral("    ");

            
            #line 28 "..\..\Views\Admin\Details.cshtml"
Write(Html.ActionLink("Edit", "Edit", new { id = Model.GetPrimaryKeyValue() }));

            
            #line default
            #line hidden
WriteLiteral(" |\r\n");

WriteLiteral("    ");

            
            #line 29 "..\..\Views\Admin\Details.cshtml"
Write(Html.ActionLink("Back to List", "Index"));

            
            #line default
            #line hidden
WriteLiteral("\r\n</p>\r\n");

        }
    }
}
#pragma warning restore 1591
