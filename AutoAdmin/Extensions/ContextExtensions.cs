﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Diagnostics;
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
        public static Type TableType(this DbContext ctx, string tableName)
        {
            return ctx.GetType().GetProperty(tableName).PropertyType.GetGenericArguments()[0];
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

                Type convertedType = property.PropertyType;
                if (property.PropertyType.IsGenericType)
                {
                    convertedType = property.PropertyType.GetGenericArguments()[0];
                }
                Debug.WriteLine("________________________________________________________________________________________");
                Debug.WriteLine($"{property.Name} will be set as {from[property.Name]} and converted to {convertedType}");
                Debug.WriteLine("________________________________________________________________________________________");
                property.SetValue(to,
                    Convert.ChangeType( from[property.Name], convertedType));
                                        //property.PropertyType.IsGenericType ? property.PropertyType.GetGenericArguments()[0] : property.PropertyType));
            }
            return to;
        }
    }
}