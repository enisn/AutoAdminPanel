using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoAdmin.Extensions
{
    public static class ContextExtensions
    {
        public static DbSet Table(this DbContext ctx, string tableName)
        {
            //return ctx.GetType().GetProperty(tableName)?.GetValue(ctx); 
            return ctx.Set(ctx.GetType().GetProperty(tableName).PropertyType.GetGenericArguments()[0]);
        }

        public static object CopyFrom(this object to, object from)
        {
            foreach (var property in from.GetType().GetProperties())
            {
                to.GetType().GetProperty(property.Name)?.SetValue(to, property.GetValue(from));
            }
            return to;
        }

        public static object CopyFrom(this object to, NameValueCollection from)
        {
            foreach (var property in to.GetType().GetProperties())
            {
                if (from[property.Name] == null) continue;
                property.SetValue(to, Convert.ChangeType(from[property.Name], property.PropertyType));
            }
            return to;
        }
    }
}