using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace AutoAdmin.Extensions
{
    public static class ViewExtensions
    {
        public static SelectList ToSelectList(this IEnumerable collection, object selected = null)
        {
            var enumerator = collection.GetEnumerator();
            enumerator.MoveNext();
            var first = enumerator.Current;
            if (first != null)
            {
                var pKeyType = first.GetType().GetPrimaryKeyType().IsGenericType ? first.GetType().GetPrimaryKeyType().GetGenericArguments()[0] : first.GetType().GetPrimaryKeyType();

                var sList = new SelectList(collection, first.GetPrimaryKeyName(), null, selected != null ? Convert.ChangeType(selected, pKeyType) : selected);
                return sList;
            }
            else
                return new SelectList(new[] { "Veri Bulunamadı!" });
        }

        public static SelectList ToSelectList(this object collection, object selected = null)
        {
            if (collection is IEnumerable && !(collection is String))
                return ToSelectList(collection as IEnumerable, selected);

            throw new InvalidCastException("Data type is not a valid IEnumerable");
        }


        public static MvcHtmlString AutoEditorFor(this HtmlHelper<object> html, PropertyInfo property)
        {

            if (property.PropertyType.IsGenericType)
                return MvcHtmlString.Empty;

            if (property.HasAttribute(typeof(KeyAttribute)))
                return html.Hidden(property.Name);
            if (property.PropertyType.IsClass && property.PropertyType != typeof(string) && property.PropertyType != typeof(IEnumerable) && !property.PropertyType.IsArray)
                return html.DropDownList(property.Name.GetTablePrimayKeyName(), html.ViewData[property.Name].ToSelectList(html.ViewData.Model.GetForeignKeyFor(property.Name)), new { @class = "selectpicker"/*, multiple = "multiple"*/ });
            return html.Editor(property.Name, new { htmlAttributes = new { @class = "form-control" } });
        }
        public static MvcHtmlString AutoDisplayFor(this HtmlHelper<object> html, PropertyInfo property)
        {
            return null;
        }
        public static MvcHtmlString AutoLabelFor(this HtmlHelper<object> html, PropertyInfo property, object htmlAttributes)
        {
            if (property.PropertyType.IsGenericType)
                return MvcHtmlString.Empty;

            return html.Label(property.Name, htmlAttributes == null ? new { @class = "control-label col-md-2" } : htmlAttributes);
        }
        public static MvcHtmlString AutoValidationMessageFor(this HtmlHelper<object> html, PropertyInfo property, object htmlAttributes = null)
        {
            if (property.PropertyType.IsGenericType)
                return MvcHtmlString.Empty;

            return html.ValidationMessage(property.Name, htmlAttributes == null ? new { @class = "text-danger" } : htmlAttributes);
        }
    }
}