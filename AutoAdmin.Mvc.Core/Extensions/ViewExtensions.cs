using AutoAdmin.Mvc.Core.Attributes;
using AutoAdmin.Mvc.Core.Helpers;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AutoAdmin.Mvc.Core.Extensions
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
                var pKeyName = first.GetPrimaryKeyName();
                //var selectedValue = selected != null ? Convert.ChangeType(selected, pKeyType) : selected;
                var sList = new SelectList(collection, pKeyName, null, selected);
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

        public static IEnumerable<PropertyInfo> GetProperties(this object model)
        {
            foreach (var property in model.GetType().GetProperties())
                yield return property;
        }
        public static PropertyInfo GetPropertyFromType(this object value, Type type)
        {
            return value.GetType().GetProperties().FirstOrDefault(x => x.PropertyType == type);
            //return value.GetType().GetProperties().FirstOrDefault(x => x.PropertyType.IsGenericType ? x.PropertyType.GetGenericArguments()[0] == type : x.PropertyType == type);
        }
        /// <summary>
        /// Generates a editor for current property
        /// </summary>
        public static IHtmlContent AutoEditorFor<T>(this IHtmlHelper<T> html, PropertyInfo property, object htmlAttributes = null)
        {
            if (property.HasAttribute(typeof(KeyAttribute)) || property.HasAttribute(typeof(IgnoreAttribute)))
                return null;

            //if (property.HasAttribute(typeof(IgnoreAttribute)))
            //    return html.Hidden(property.Name);

            var _relation = property.GetRelation();
            switch (_relation)
            {
                case Relation.OneToOne:
                    return html.Editor(property.Name, new { htmlAttributes = htmlAttributes ?? new { @class = "form-control" } });
                case Relation.OneToMany:
                    //dropdown
                    return html.ActionLink("OneToMany Action", "Edit", "Admin", new { table = property.PropertyType.Name }, new { @class = "btn btn-primary" });

                case Relation.ManyToOne:

                    return html.DropDownList(

                        property.Name,

                        html.ViewData[property.Name]
                                    .ToSelectList(property.GetValue(html.ViewData.Model)?.GetPrimaryKeyValue()),
                                      new { @class = "selectpicker form-control" }
                        );
                case Relation.ManyToMany:
                    return html.DropDownList(html.ViewData.Model.GetPropertyFromType(property.PropertyType)?.Name,
                       html.ViewData[property.Name].ToSelectList(),
                       new { @class = "selectpicker form-control", multiple = "multiple" });
                case Relation.None:
                    if (property.PropertyType == typeof(bool))
                        //return IHtmlContent.Create("");
                        return new HtmlContentBuilder().AppendHtml("");
                    return html.Editor(property.Name, new { htmlAttributes = htmlAttributes ?? new { @class = "form-control" } });
                default:
                    break;
            }
            return html.ActionLink("Could not found editor for", "Index", "Admin", new { table = property.DeclaringType.Name });
        }
        public static IHtmlContent AutoDisplayFor<T>(this IHtmlHelper<T> html, PropertyInfo property, object htmlAttributes = null)
        {
            if (property.HasAttribute(typeof(KeyAttribute)) || property.HasAttribute(typeof(IgnoreAttribute)))
                return null;

            switch (property.GetRelation())
            {
                case Relation.None:
                    return html.Display(property.Name);
                case Relation.OneToOne:
                    return html.ActionLink(/*property.GetValue(html.ViewData.Model)?.ToString()*/"View", "Details", new { table = property.PropertyType.Name, id = property.GetValue(html.ViewData.Model)?.GetPrimaryKeyValue() });

                case Relation.OneToMany:
                    return html.ActionLink(/*property.GetValue(html.ViewData.Model)?.ToString()*/"View All", "Index", new RouteValueDictionary(new Dictionary<string, object>()
                    {
                        { "table",( property.PropertyType.IsConstructedGenericType ?  property.PropertyType.GetGenericArguments()[0] : property.PropertyType).GetTableName() },
                        { property.DeclaringType.GetTypePrimaryKeyName(),$"'{html.ViewData.Model.GetPrimaryKeyValue()}'" },
                    }));

                case Relation.ManyToOne:
                case Relation.ManyToMany:
                    return html.Display(property.Name);
            }
            return html.Display(property.Name, null, null, null);
        }
        public static IHtmlContent AutoLabelFor<T>(this IHtmlHelper<T> html, PropertyInfo property, object htmlAttributes = null)
        {
            if (property.HasAttribute(typeof(KeyAttribute)) || property.HasAttribute(typeof(IgnoreAttribute)))
                return null;

            return html.Label(property.Name, null, htmlAttributes == null ? new { @class = "control-label col-md-2" } : htmlAttributes);


        }
        public static IHtmlContent AutoValidationMessageFor<T>(this IHtmlHelper<T> html, PropertyInfo property, object htmlAttributes = null)
        {
            if (property.HasAttribute(typeof(KeyAttribute)) || property.HasAttribute(typeof(IgnoreAttribute)))
                return null;

            return html.ValidationMessage(property.Name, htmlAttributes == null ? new { @class = "text-danger" } : htmlAttributes);

        }
    }
}
