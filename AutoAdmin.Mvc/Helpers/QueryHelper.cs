using AutoAdmin.Mvc.Attributes;
using AutoAdmin.Mvc.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;

namespace AutoAdmin.Mvc.Helpers
{
    public class QueryHelper
    {
        public static object Get(string table, object id)
        {
            //using (var ctx = Configuration.NewContext())
            //{
            //    return ctx.Table(table).Find(id);
            //}
            var _pKeyType = Configuration.Context.TableTypeOf(table).GetPrimaryKeyType();
            if (_pKeyType.IsGenericType)
                _pKeyType = _pKeyType.GetGenericArguments()[0];

            return Configuration.Context.Table(table).Find(Convert.ChangeType(id, _pKeyType));
        }
        public static object GetInstance(string table)
        {
            Type _type = Configuration.ctxType.GetProperty(table).PropertyType.GetGenericArguments()[0];
            return Activator.CreateInstance(_type);
        }
        public static IEnumerable<PropertyInfo> GetRelationProperties(string table)
        {
            var tableType = Configuration.ctxType.GetProperty(table).PropertyType.GetGenericArguments()[0];
            foreach (var property in tableType.GetProperties())
            {
                if (tableType.IsDropdownValue(property))
                {
                    yield return property;
                }
            }
        }
        public static IEnumerable GetMultiple(string table)
        {
            using (var ctx = Configuration.NewContext())
            {
                try
                {
                    return ctx.Table(table).ToListAsync().Result;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    return new[] { GetInstance(table) };
                }
            }
        }

        public static IEnumerable GetMultiple(string table, params object[] ids)
        {
            //using (var ctx = Configuration.NewContext())
            //{
            var ctx = Configuration.Context;
            string query = $"SELECT * FROM dbo.[{table.Replace('_', ' ')}] WHERE ";
            foreach (string id in ids)
            {
                query += $"{table.GetTablePrimayKeyName()} = {id} OR ";
            }
            query = query.Remove(query.Length - 4, 3);

            var result = ctx.Table(table).SqlQuery(query).ToListAsync().Result;
            return result;
            //}
        }


        public static IEnumerable GetMultiple(string table, NameValueCollection filters)
        {
            if (filters?.Count > 0 == false)
                return GetMultiple(table);

            //using (var ctx = Configuration.NewContext())
            //{
            var ctx = Configuration.Context;

            string query = $"SELECT * FROM dbo.[{table.Replace('_', ' ')}] WHERE ";
            foreach (string key in filters)
            {
                query += $"{key} = {filters[key]} AND ";
            }
            query = query.Remove(query.Length - 5, 4);

            var result = ctx.Table(table).SqlQuery(query).ToListAsync().Result;
            return result;
            //}
        }

        public static IEnumerable GetMultiple(Type tableType)
        {
            //using (var ctx = Configuration.NewContext())
            //{
            var ctx = Configuration.Context;
            if (tableType.IsGenericType) tableType = tableType.GetGenericArguments()[0];
            return ctx.Set(tableType).ToListAsync().Result;
            //}
        }
        public static void Add(string table, object entity)
        {
            //using (var ctx = (DbContext)Activator.CreateInstance(Configuration.ctxType))
            //{
            var ctx = Configuration.Context;
            ctx.Set(Configuration.ctxType.GetProperty(table).PropertyType.GetGenericArguments()[0]).Add(entity);
            ctx.SaveChanges();
            //}
        }

        public static void Delete(string table, object id)
        {
            //using (var ctx = Configuration.NewContext())
            //{
            var ctx = Configuration.Context;
            var _pKeyType = ctx.TableTypeOf(table).GetPrimaryKeyType();
            if (_pKeyType.IsGenericType)
                _pKeyType = _pKeyType.GetGenericArguments()[0];

            ctx.Table(table).Remove(ctx.Table(table).Find(Convert.ChangeType(id, _pKeyType)));
            ctx.SaveChanges();
            //}
            //Configuration.Context.Table(table).Remove(Get(table, id));
            //Configuration.Context.SaveChanges();
        }
        public static void Update(string table, object entity, object id = null)
        {
            //using (var ctx = Configuration.NewContext())
            //{
            var ctx = Configuration.Context;

            object editedEntity;
            if (id != null)
            {
                editedEntity = ctx.Table(table).Find(Convert.ChangeType(id, ctx.TableTypeOf(table).GetPrimaryKeyType()));
            }
            else
            {
                editedEntity = ctx.Table(table).Find(entity);
            }

            editedEntity.CopyFrom(entity);
            ctx.SaveChanges();
            //}
        }

        public static IEnumerable<string> GetTableNames()
        {
            foreach (var property in Configuration.ctxType.GetProperties())
            {
                if (property.HasAttribute(typeof(IgnoreAttribute)))
                    continue;
                yield return property.Name;
            }

        }
    }
}