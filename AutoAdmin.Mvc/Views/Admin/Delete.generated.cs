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
    using System.Web.WebPages;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Admin/Delete.cshtml")]
    public partial class _Views_Admin_Delete_cshtml : System.Web.Mvc.WebViewPage<object>
    {
        public _Views_Admin_Delete_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Views\Admin\Delete.cshtml"
  
    ViewBag.Title = "Delete";
    Layout = "~/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<h2>Delete</h2>\r\n\r\n<h3>Are you sure you want to delete this?</h3>\r\n<div>\r\n   " +
" <h4>Categories</h4>\r\n    <hr />\r\n    <dl");

WriteLiteral(" class=\"dl-horizontal\"");

WriteLiteral(">\r\n\r\n");

            
            #line 16 "..\..\Views\Admin\Delete.cshtml"
        
            
            #line default
            #line hidden
            
            #line 16 "..\..\Views\Admin\Delete.cshtml"
         foreach (var property in Model.GetType().GetProperties())
        {
            if (property.PropertyType.IsGenericType) { continue; }


            
            #line default
            #line hidden
WriteLiteral("            <dt>\r\n");

WriteLiteral("                ");

            
            #line 21 "..\..\Views\Admin\Delete.cshtml"
           Write(Html.DisplayName(property.Name));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </dt>\r\n");

            
            #line 23 "..\..\Views\Admin\Delete.cshtml"


            
            #line default
            #line hidden
WriteLiteral("            <dd>\r\n");

WriteLiteral("                ");

            
            #line 25 "..\..\Views\Admin\Delete.cshtml"
           Write(Html.Display(property.Name));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </dd>\r\n");

            
            #line 27 "..\..\Views\Admin\Delete.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("\r\n    </dl>\r\n\r\n");

            
            #line 31 "..\..\Views\Admin\Delete.cshtml"
    
            
            #line default
            #line hidden
            
            #line 31 "..\..\Views\Admin\Delete.cshtml"
     using (Html.BeginForm())
    {
        
            
            #line default
            #line hidden
            
            #line 33 "..\..\Views\Admin\Delete.cshtml"
   Write(Html.AntiForgeryToken());

            
            #line default
            #line hidden
            
            #line 33 "..\..\Views\Admin\Delete.cshtml"
                                


            
            #line default
            #line hidden
WriteLiteral("        <div");

WriteLiteral(" class=\"form-actions no-color\"");

WriteLiteral(">\r\n            <input");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" value=\"Delete\"");

WriteLiteral(" class=\"btn btn-default\"");

WriteLiteral(" /> |\r\n");

WriteLiteral("            ");

            
            #line 37 "..\..\Views\Admin\Delete.cshtml"
       Write(Html.ActionLink("Back to List", "Index"));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </div>\r\n");

            
            #line 39 "..\..\Views\Admin\Delete.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n");

        }
    }
}
#pragma warning restore 1591
