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
    
    #line 4 "..\..\Views\Admin\Index.cshtml"
    using AutoAdmin.Mvc;
    
    #line default
    #line hidden
    
    #line 3 "..\..\Views\Admin\Index.cshtml"
    using AutoAdmin.Mvc.Extensions;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Admin/Index.cshtml")]
    public partial class _Views_Admin_Index_cshtml : System.Web.Mvc.WebViewPage<IEnumerable<object>>
    {
        public _Views_Admin_Index_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("    ");

WriteLiteral("    ");

WriteLiteral("    ");

            
            #line 5 "..\..\Views\Admin\Index.cshtml"
      
        ViewBag.Title = "Index";
        Layout = "~/Views/Shared/_Layout.cshtml";
    
            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n    <h2>Index</h2>\r\n\r\n    <p>\r\n");

WriteLiteral("        ");

            
            #line 13 "..\..\Views\Admin\Index.cshtml"
   Write(Html.ActionLink("Create New", "Create", null, new { @class = Configuration.IndexConfiguration.AddClass }));

            
            #line default
            #line hidden
WriteLiteral("\r\n    </p>\r\n    <table");

WriteAttribute("class", Tuple.Create(" class=\"", 363), Tuple.Create("\"", 415)
            
            #line 15 "..\..\Views\Admin\Index.cshtml"
, Tuple.Create(Tuple.Create("", 371), Tuple.Create<System.Object, System.Int32>(Configuration.IndexConfiguration.TableClass
            
            #line default
            #line hidden
, 371), false)
);

WriteLiteral(">\r\n        <tr>\r\n");

            
            #line 17 "..\..\Views\Admin\Index.cshtml"
            
            
            #line default
            #line hidden
            
            #line 17 "..\..\Views\Admin\Index.cshtml"
               var properties = Model.FirstOrDefault()?.GetType().GetProperties(); 
            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 18 "..\..\Views\Admin\Index.cshtml"
            
            
            #line default
            #line hidden
            
            #line 18 "..\..\Views\Admin\Index.cshtml"
             if (properties != null)
            {

                foreach (var property in properties)
                {
                    if (property.PropertyType.IsGenericType || (property.PropertyType != typeof(string) && property.PropertyType.IsClass))
                    {
                        continue;
                    }

            
            #line default
            #line hidden
WriteLiteral("                    <th>\r\n");

WriteLiteral("                        ");

            
            #line 28 "..\..\Views\Admin\Index.cshtml"
                   Write(property.Name);

            
            #line default
            #line hidden
WriteLiteral("\r\n                        ");

WriteLiteral("\r\n                    </th>\r\n");

            
            #line 31 "..\..\Views\Admin\Index.cshtml"
                }

            }

            
            #line default
            #line hidden
WriteLiteral("            <th></th>\r\n        </tr>\r\n\r\n");

            
            #line 37 "..\..\Views\Admin\Index.cshtml"
        
            
            #line default
            #line hidden
            
            #line 37 "..\..\Views\Admin\Index.cshtml"
         foreach (var item in Model)
        {

            
            #line default
            #line hidden
WriteLiteral("            <tr>\r\n\r\n");

            
            #line 41 "..\..\Views\Admin\Index.cshtml"
                
            
            #line default
            #line hidden
            
            #line 41 "..\..\Views\Admin\Index.cshtml"
                 foreach (var property in properties)
                {
                    if (property.PropertyType.IsGenericType || (property.PropertyType != typeof(string) && property.PropertyType.IsClass))
                    {
                        continue;
                    }

            
            #line default
            #line hidden
WriteLiteral("                    <td>\r\n                        ");

WriteLiteral("\r\n                        ");

WriteLiteral("\r\n");

WriteLiteral("                        ");

            
            #line 50 "..\..\Views\Admin\Index.cshtml"
                   Write(property.GetValue(item));

            
            #line default
            #line hidden
WriteLiteral("\r\n                    </td>\r\n");

            
            #line 52 "..\..\Views\Admin\Index.cshtml"
                }

            
            #line default
            #line hidden
WriteLiteral("\r\n                <td>\r\n");

WriteLiteral("                    ");

            
            #line 55 "..\..\Views\Admin\Index.cshtml"
               Write(Html.ActionLink("Edit", "Edit", new { id = item.GetPrimaryKeyValue() }, new { @class = Configuration.IndexConfiguration.EditClass }));

            
            #line default
            #line hidden
WriteLiteral(" |\r\n");

WriteLiteral("                    ");

            
            #line 56 "..\..\Views\Admin\Index.cshtml"
               Write(Html.ActionLink("Details", "Details", new { id = item.GetPrimaryKeyValue() }, new { @class = Configuration.IndexConfiguration.DetailClass }));

            
            #line default
            #line hidden
WriteLiteral(" |\r\n");

WriteLiteral("                    ");

            
            #line 57 "..\..\Views\Admin\Index.cshtml"
               Write(Html.ActionLink("Delete", "Delete", new { id = item.GetPrimaryKeyValue() }, new { @class = Configuration.IndexConfiguration.DeleteClass }));

            
            #line default
            #line hidden
WriteLiteral("\r\n                </td>\r\n            </tr>\r\n");

            
            #line 60 "..\..\Views\Admin\Index.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("\r\n    </table>\r\n");

        }
    }
}
#pragma warning restore 1591
