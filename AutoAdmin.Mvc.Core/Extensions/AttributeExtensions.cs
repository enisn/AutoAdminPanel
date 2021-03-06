﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AutoAdmin.Mvc.Core.Extensions
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
            return value.GetProperties().FirstOrDefault(x => x.Name.ToUpperInvariant().EndsWith("ID"))?.PropertyType;
        }
        public static string GetPrimaryKeyName(this Type type)
        {
            if (type.IsGenericType)
                type = type.GetGenericArguments()[0];

            foreach (var property in type.GetProperties())
            {
                if (property.HasAttribute(typeof(KeyAttribute)))
                    return property.Name;
            }

            return type.GetProperties().FirstOrDefault(x => x.Name.ToUpperInvariant().EndsWith("ID")).Name;
        }

        public static string GetTypePrimaryKeyName(this Type type)
        {
            if (type.IsGenericType)
                type = type.GetGenericArguments()[0];
            foreach (var property in type.GetProperties())
            {
                if (property.HasAttribute(typeof(KeyAttribute)))
                    return property.Name;
            }
            var _primaryKey = type.GetProperties().FirstOrDefault(x => x.Name.ToUpperInvariant().EndsWith("ID"));
            if (_primaryKey == null)
                throw new Exception($"Primary key could not found in {type.Name} as {type.Name}ID", new Exception("If you get this error, you should use [Key] attribute in your model to declare PrimaryKey"));
            return _primaryKey.Name;
        }
        public static string GetTablePrimayKeyName(this string table)
        {
            var properties = Configuration.Context.TableTypeOf(table).GetProperties();
            foreach (var property in properties)
            {
                if (property.HasAttribute(typeof(KeyAttribute)))
                    return property.Name;
            }
            var _primaryKey = properties.FirstOrDefault(x => x.Name.ToUpperInvariant().EndsWith("ID"));
            if (_primaryKey == null)
                throw new Exception($"Primary key could not found in {table} as {table}ID", new Exception("If you get this error, you should use [Key] attribute in your model to declare PrimaryKey"));
            return _primaryKey.Name;
        }
        public static object GetPrimaryKeyValue(this object value)
        {
            return value.GetType().GetProperty(value.GetPrimaryKeyName())?.GetValue(value);
        }
        public static object GetForeignKeyFor(this object value, Type propertyType)
        {
            if (propertyType.IsGenericType)
                propertyType = propertyType.GetGenericArguments()[0];
            //var _property = Configuration.ctxType.GetProperties().FirstOrDefault(x => x.PropertyType.IsGenericParameter ? x.PropertyType.GetGenericArguments()[0] == propertyType : x.PropertyType == propertyType);
            var _property = value.GetType().GetProperty(propertyType.GetPrimaryKeyName());
            if (_property == null)
                throw new Exception($"Foreign key could not found for {value.GetType().Name} as {_property.Name}", new Exception("You should use [Key] attribute in your models for define Primary Keys and [Foreign] attribute to declare foreign keys into your models if default find algorithm can't work."));
            return _property?.GetValue(value);
        }
        public static object GetForeignKeyFor(this object value, string table)
        {
            var pForeignKeyName = Configuration.ctxType.GetProperty(table).PropertyType.IsGenericType ? Configuration.ctxType.GetProperty(table).PropertyType.GetGenericArguments()[0].GetPrimaryKeyName() : Configuration.ctxType.GetProperty(table).PropertyType.GetPrimaryKeyName();
            var _foreignKeyInfo = value.GetType().GetProperty(pForeignKeyName);
            if (_foreignKeyInfo == null)
                throw new Exception($"Foreign key could not found for {table} as {pForeignKeyName}", new Exception("You should use [Key] attribute in your models for define Primary Keys and [Foreign] attribute to declare foreign keys into your models if default find algorithm can't work."));
            return _foreignKeyInfo.GetValue(value);
        }
        public static string GetForeignKeyName(this Type type, string table)
        {
            return type.GetProperty(Configuration.ctxType.GetProperty(table).PropertyType.GetGenericArguments()[0].Name)?.Name;
        }

        public static IEnumerable SelectPrimaryKeys(this IEnumerable collection)
        {
            foreach (var item in collection)
            {
                yield return item.GetPrimaryKeyValue();
            }
        }
        public static string ToString(this IEnumerable value, char seperator)
        {

            var enumerator = value.GetEnumerator();
            enumerator.MoveNext();
            string result = enumerator.Current?.ToString();
            while (enumerator.MoveNext())
            {
                result += seperator + enumerator.Current?.ToString();
            }
            return result;
        }
    }
}
