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
        public static dynamic Table(this DbContext ctx, string tableName)
        {
            return ctx.GetType().GetProperty(tableName)?.GetValue(ctx); 
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
                property.SetValue(to, from[property.Name]);
            }
            return to;
        }
    }
}