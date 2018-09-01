using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace AutoAdmin.Extensions
{
    public static class AttributeExtensions
    {
        public static bool HasAttribute(this MemberInfo value,Type attribute)
        {
           return value.GetCustomAttributes(attribute, false).Count() > 0;
        }

        public static string GetPrimaryKeyName(this object value)
        {
            foreach (var property in value.GetType().GetProperties())
            {
                if (property.HasAttribute(typeof(KeyAttribute)))
                    return property.Name;
            }

            return value.GetType().GetProperties().FirstOrDefault(x => x.Name.ToUpperInvariant().EndsWith("ID")).Name;
        }

        public static string GetTablePrimayKeyName(this string table)
        {
            var properties = Configuration.Context.TableType(table).GetProperties();
            foreach (var property in properties)
            {
                if (property.HasAttribute(typeof(KeyAttribute)))
                    return property.Name;
            }

            return properties.FirstOrDefault(x => x.Name.ToUpperInvariant().EndsWith("ID"))?.Name;
        }
        public static object GetPrimaryKey(this object value)
        {
            return value.GetType().GetProperty(value.GetPrimaryKeyName())?.GetValue(value);
        }
    }
}