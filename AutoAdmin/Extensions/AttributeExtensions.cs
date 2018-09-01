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
        public static bool HasAttribute(this MemberInfo value, Type attribute)
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

        public static Type GetPrimaryKeyType(this Type value)
        {
            foreach (var property in value.GetProperties())
            {
                if (property.HasAttribute(typeof(KeyAttribute)))
                    return property.PropertyType;
            }

            return value.GetProperties().FirstOrDefault(x => x.Name.ToUpperInvariant().EndsWith("ID")).PropertyType;
        }
        public static string GetPrimaryKeyName(this Type value)
        {
            foreach (var property in value.GetProperties())
            {
                if (property.HasAttribute(typeof(KeyAttribute)))
                    return property.Name;
            }

            return value.GetProperties().FirstOrDefault(x => x.Name.ToUpperInvariant().EndsWith("ID")).Name;
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

        public static object GetForeignKeyFor(this object value, string table)
        {
            var pForeignKey = Configuration.ctxType.GetProperty(table).PropertyType.GetGenericArguments()[0].GetPrimaryKeyName();
            return value.GetType().GetProperty(pForeignKey).GetValue(value);

            //var pKeyName = Configuration.ctxType.GetProperty(table).GetPrimaryKeyName();
            //return value.GetType().GetProperty(pKeyName).GetValue(value);
        }
        public static string GetForeignKeyName(this Type type, string table)
        {
            return type.GetProperty(Configuration.ctxType.GetProperty(table).PropertyType.GetGenericArguments()[0].Name).Name;
        }
    }
}