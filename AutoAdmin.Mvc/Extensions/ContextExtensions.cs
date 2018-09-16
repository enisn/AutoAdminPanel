using AutoAdmin.Mvc.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

namespace AutoAdmin.Mvc.Extensions
{
    public static class ContextExtensions
    {
        /// <summary>
        /// To get DbSet from tablename
        /// </summary>
        /// <param name="ctx"> Current Context</param>
        /// <param name="tableName">Table name to get DbSet</param>
        /// <returns></returns>
        public static DbSet Table(this DbContext ctx, string tableName)
        {
            return ctx.Set(ctx.GetType().GetProperty(tableName).PropertyType.GetGenericArguments()[0]);
        }
        public static IQueryable GenericTable(this DbContext ctx, string tableName)
        {
            var property = ctx.GetType().GetProperty(tableName);
            var method = ctx.GetType().GetMethod("Set", new Type[0]).MakeGenericMethod(property.PropertyType);
            return (IQueryable)method.Invoke(ctx, new object[0]);
        }
        /// <summary>
        /// To find table T type from DbSet&lt;T&gt; 
        /// </summary>
        /// <param name="ctx">Current DbContext</param>
        /// <param name="tableName">Table name to find type</param>
        /// <returns></returns>
        public static Type TableTypeOf(this DbContext ctx, string tableName)
        {
            return ctx.GetType().GetProperty(tableName)?.PropertyType.GetGenericArguments()[0];
        }
        public static string GetTableName(this Type type)
        {
            if (type.IsValueType || type == typeof(String) || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>)))
                return null;
            return Configuration.ctxType.GetProperties().FirstOrDefault(x => x.PropertyType.IsGenericType && x.PropertyType.GetGenericArguments()[0] == (type.IsGenericType ? type.GetGenericArguments()[0] : type))?.Name;
        }
        /// <summary>
        /// Copies all propertis from different object to this object
        /// </summary>
        /// <param name="to">Properties will be copied to this object</param>
        /// <param name="from">Properties will be copied from</param>
        /// <returns></returns>
        public static object CopyFrom(this object to, object from)
        {
            foreach (var property in from.GetType().GetProperties())
            {
                to.GetType().GetProperty(property.Name)?.SetValue(to, property.GetValue(from));
            }
            return to;
        }
        /// <summary>
        /// Copies all propertis from NameValueCollection to this object
        /// </summary>
        /// <param name="to">Properties will be copied to this object</param>
        /// <param name="from">Properties will be copied from</param>
        /// <param name="tableName">Properties will be copied to tablename</param>
        /// <returns></returns>
        public static object CopyFrom(this object to, NameValueCollection from, string tableName)
        {
            foreach (var property in to.GetType().GetProperties())
            {
                try
                {
                    if (from[property.Name] == null) continue;
                    var relation = property.GetRelation();
                    Type convertedType = property.PropertyType;
                    if (property.PropertyType.IsGenericType)
                        convertedType = property.PropertyType.GetGenericArguments()[0];


#if DEBUG
                    var _test = from[property.Name];
#endif
                    switch (relation)
                    {

                        case Relation.OneToOne:
                        case Relation.ManyToOne:
                            property.SetValue(to, QueryHelper.Get(tableName, from[property.Name]));
                            break;
                        case Relation.OneToMany:
                        case Relation.ManyToMany:
                            var _add = property.PropertyType.GetMethod("Add");
                            foreach (var id in from[property.Name].Split(','))
                                _add.Invoke(property.GetValue(to), parameters: new[] { QueryHelper.Get(property.PropertyType.GetTableName(), id) });
                            break;
                        case Relation.None:
                            {
                                if (convertedType == typeof(Boolean)) //for the html CheckBox "true,false" bug
                                    property.SetValue(to, from[property.Name].Contains("true"));
                                else
                                    property.SetValue(to,
                                        Convert.ChangeType(from[property.Name], convertedType));
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    continue;
                }
            }
            return to;
        }
        /// <summary>
        /// Copies all propertis from different object to this object. If an error occours. Returns false at the end of proccess
        /// </summary>
        /// <param name="to">Properties will be copied to this object</param>
        /// <param name="from">Properties will be copied from</param>
        /// <returns></returns>
        public static bool TryCopyFrom(this object to, NameValueCollection from)
        {
            bool result = true;
            foreach (var property in to.GetType().GetProperties())
            {
                try
                {
                    if (from[property.Name] == null) continue;

                    Type convertedType = property.PropertyType;
                    if (property.PropertyType.IsGenericType)
                        convertedType = property.PropertyType.GetGenericArguments()[0];

#if DEBUG
                    var _test = from[property.Name];
#endif

                    if (convertedType == typeof(bool)) //for the html CheckBox "true,false" bug from hidden chechbox for bootstrap
                        property.SetValue(to, from[property.Name].Contains("true"));
                    else
                        property.SetValue(to,
                                        Convert.ChangeType(from[property.Name], convertedType));
                }
                catch (Exception ex)
                {
                    result = false;
                    Debug.WriteLine(ex.ToString());
                }
            }
            return result;
        }
        public static bool IsDropdownValue(this Type value, PropertyInfo property)
        {
            var relation = property.GetRelation();

            return relation == Relation.ManyToOne || relation == Relation.ManyToMany;
        }
    }
}