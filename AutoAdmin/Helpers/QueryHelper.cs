using AutoAdmin.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using System.Threading.Tasks;
using System.Web;

namespace AutoAdmin.Helpers
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
        public static IEnumerable<string> GetRelationsNames(string table)
        {
            foreach (var property in Configuration.ctxType.GetProperty(table).PropertyType.GetGenericArguments()[0].GetProperties())
            {
                //if (Configuration.ctxType.GetProperties().Any(a => a.Name == property.Name) && property.PropertyType != typeof(ICollection))
                if (Configuration.ctxType.GetProperties().Any(a => a.Name == property.Name) && !property.PropertyType.IsGenericType)
                {
                    yield return property.Name;
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
        public static void Add(string table, object entity)
        {
            using (var ctx = (DbContext)Activator.CreateInstance(Configuration.ctxType))
            {
                ctx.Set(Configuration.ctxType.GetProperty(table).PropertyType.GetGenericArguments()[0]).Add(entity);
                ctx.SaveChanges();
            }
        }

        public static void Delete(string table, object id)
        {
            using (var ctx = Configuration.NewContext())
            {
                var _pKeyType = ctx.TableTypeOf(table).GetPrimaryKeyType();
                if (_pKeyType.IsGenericType)
                    _pKeyType = _pKeyType.GetGenericArguments()[0];

                ctx.Table(table).Remove(ctx.Table(table).Find(Convert.ChangeType(id, _pKeyType)));
                ctx.SaveChanges();
            }
            //Configuration.Context.Table(table).Remove(Get(table, id));
            //Configuration.Context.SaveChanges();
        }
        public static void Update(string table, object entity, object id = null)
        {
            using (var ctx = Configuration.NewContext())
            {

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
            }
        }

        public static IEnumerable<string> GetTableNames()
        {

            foreach (var property in Configuration.ctxType.GetProperties())
            {
                yield return property.Name;
            }

        }
    }
}