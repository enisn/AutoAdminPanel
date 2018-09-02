using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
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
            //return ctx.GetType().GetProperty(tableName)?.GetValue(ctx); 
            return ctx.Set(ctx.GetType().GetProperty(tableName).PropertyType.GetGenericArguments()[0]);
        }
        /// <summary>
        /// To find table T type from DbSet&lt;T&gt; 
        /// </summary>
        /// <param name="ctx">Current DbContext</param>
        /// <param name="tableName">Table name to find type</param>
        /// <returns></returns>
        public static Type TableTypeOf(this DbContext ctx, string tableName)
        {
            return ctx.GetType().GetProperty(tableName).PropertyType.GetGenericArguments()[0];
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
        /// <returns></returns>
        public static object CopyFrom(this object to, NameValueCollection from)
        {
            foreach (var property in to.GetType().GetProperties())
            {
                if (from[property.Name] == null) continue;

                Type convertedType = property.PropertyType;
                if (property.PropertyType.IsGenericType)
                {
                    convertedType = property.PropertyType.GetGenericArguments()[0];
                }
                if (convertedType == typeof(Boolean)) //for the html CheckBox "true,false" bug
                    property.SetValue(to, from[property.Name].Contains("true"));
                else
                    property.SetValue(to,
                        Convert.ChangeType(from[property.Name], convertedType));
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
                    {
                        convertedType = property.PropertyType.GetGenericArguments()[0];
                    }
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
    }
}