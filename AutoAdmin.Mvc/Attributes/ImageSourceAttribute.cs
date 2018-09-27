using AutoAdmin.Mvc.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AutoAdmin.Mvc.Attributes
{
    [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class ImageSourceAttribute : Attribute, IModelAttribute
    {
        public ImageSourceAttribute()
        {

        }
        public string EditorClass { get; set ; }
        public string DisplayClass { get; set ; }
        public bool IsValidated { get ; set ; }
        public Func<dynamic, bool> ValidationExpression { get ; set ; }

        public MvcHtmlString GetDisplayHtml(object value)
        {
            return MvcHtmlString.Create($"<a href=\"{value}\"> <img src=\"{value}\" alt=\"\" class=\"{this.DisplayClass}\" /> </a> ");
        }

        public MvcHtmlString GetEditorHtml(object value)
        {
            return MvcHtmlString.Create($"<input name=\"{value}\" type=\"file\" accept=\"image/*\" />");
        }

        public MvcHtmlString GetValidationHtml(object value)
        {
            return MvcHtmlString.Create($"<label>{value}</label>");
        }
    }
}
