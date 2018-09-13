using AutoAdmin.Mvc.Attributes;
using AutoAdmin.Mvc.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace AutoAdmin.Mvc.Extensions
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
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public static MvcHtmlString AutoEditorFor(this HtmlHelper<object> html, PropertyInfo property, object htmlAttributes = null)
        {
            if (property.HasAttribute(typeof(KeyAttribute)))
                return html.Hidden(property.Name);
            if ((property.PropertyType.IsGenericType || property.PropertyType.GetTableName() != null) && !html.ViewData.ContainsKey(property.Name))
                return MvcHtmlString.Empty;

            if (html.ViewData.Model.IsDropdownValue(property))
            {
                if (html.ViewData.Model.IsManyToManyWith(property.Name))
                {
                    var asd = (property.GetValue(html.ViewData.Model) as IEnumerable).SelectPrimaryKeys();
                    return html.DropDownList(html.ViewData.Model.GetPropertyFromType(property.PropertyType)?.Name,
                        html.ViewData[property.Name].ToSelectList(),
                        new { @class = "selectpicker", multiple = "multiple" });
                }
                else
                    return html.DropDownList(

                        property.PropertyType.GetTypePrimaryKeyName(),

                        html.ViewData[property.Name]
                                    .ToSelectList(html.ViewData.Model.GetForeignKeyFor(property.PropertyType)),
                                      new { @class = "selectpicker" }
                        );
            }

            //if (!property.PropertyType.IsGenericType || (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)))
            return html.Editor(property.Name, new { htmlAttributes = new { @class = "form-control" } });

        }

        public static MvcHtmlString AutoEditorFor(this HtmlHelper<object> html, PropertyInfo property, bool newVal)
        {
            if (property.HasAttribute(typeof(KeyAttribute)) || property.HasAttribute(typeof(IgnoreAttribute)))
                return html.Hidden(property.Name);


            var _relation = property.GetRelation();
            switch (_relation)
            {
                case Relation.OneToOne:
                    return html.ActionLink("OneToOne Action", "Edit", "Admin", new { table = property.PropertyType.Name }, new { @class = "btn btn-primary" });
                case Relation.OneToMany:
                    //dropdown
                    return html.ActionLink("OneToMany Action", "Edit", "Admin", new { table = property.PropertyType.Name }, new { @class = "btn btn-primary" });

                case Relation.ManyToOne:

                    return html.DropDownList(

                        property.PropertyType.GetTypePrimaryKeyName(),

                        html.ViewData[property.Name]
                                    .ToSelectList(html.ViewData.Model.GetForeignKeyFor(property.PropertyType)),
                                      new { @class = "selectpicker" }
                        );
                case Relation.ManyToMany:
                    return html.DropDownList(html.ViewData.Model.GetPropertyFromType(property.PropertyType)?.Name,
                       html.ViewData[property.Name].ToSelectList(),
                       new { @class = "selectpicker", multiple = "multiple" });
                case Relation.None:
                    return html.Editor(property.Name);
                default:
                    break;
            }
            return html.ActionLink("Could not found editor for", "Index", "Admin", new { table = property.DeclaringType.Name });
        }
        public static MvcHtmlString AutoDisplayFor(this HtmlHelper<object> html, PropertyInfo property)
        {
            if ((property.PropertyType.IsGenericType || property.PropertyType.GetTableName() != null) && !html.ViewData.ContainsKey(property.Name))
                return MvcHtmlString.Empty;

            return html.Display(property.Name);

        }
        public static MvcHtmlString AutoLabelFor(this HtmlHelper<object> html, PropertyInfo property, object htmlAttributes = null)
        {
            if (property.HasAttribute(typeof(KeyAttribute)) || property.HasAttribute(typeof(IgnoreAttribute)))
                return MvcHtmlString.Empty;

            return html.Label(property.Name, htmlAttributes == null ? new { @class = "control-label col-md-2" } : htmlAttributes);


        }
        public static MvcHtmlString AutoValidationMessageFor(this HtmlHelper<object> html, PropertyInfo property, object htmlAttributes = null)
        {
            if (property.HasAttribute(typeof(KeyAttribute)) || property.HasAttribute(typeof(IgnoreAttribute)))
                return MvcHtmlString.Empty;

            return html.ValidationMessage(property.Name, htmlAttributes == null ? new { @class = "text-danger" } : htmlAttributes);

        }
    }
}