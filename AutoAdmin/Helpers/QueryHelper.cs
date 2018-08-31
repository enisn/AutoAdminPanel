﻿using AutoAdmin.Extensions;
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
            return Configuration.Context.Table(table).Find(id);
        }
        public static object GetInstance(string table)
        {
            Type _type = Configuration.Context.GetType().GetProperty(table).PropertyType.GetGenericArguments()[0];
            return Activator.CreateInstance(_type);
        }
        public static IEnumerable<string> GetRelationsNames(string table)
        {
            foreach (var property in Configuration.Context.GetType().GetProperty(table).PropertyType.GetGenericArguments()[0].GetProperties())
            {
                if (Configuration.Context.GetType().GetProperties().Any(a=> a.Name == property.Name) && property.PropertyType != typeof(IEnumerable))
                {                    
                    yield return property.Name;
                }
            }
        }
        public static IEnumerable GetMultiple(string table)
        {
            return Configuration.Context.Table(table) as IEnumerable;
        }
        public static void Add(string table, object entity)
        {
            Configuration.Context.Table(table).Add(entity);
            Configuration.Context.SaveChanges();
        }

        public static void Delete(string table, object id)
        {
            Configuration.Context.Table(table).Remove(Get(table, id));
            Configuration.Context.SaveChanges();
        }
        public static void Update(string table, object entity, object id = null)
        {
            object editedEntity;
            if (id != null)
            {
                editedEntity = Get(table, id);
            }
            else
            {
                editedEntity = Configuration.Context.Table(table).Find(entity);
            }

            editedEntity.CopyFrom(entity);
            Configuration.Context.SaveChanges();
        }

        public static IEnumerable<string> GetTableNames()
        {
            foreach (var property in Configuration.Context.GetType().GetProperties())
            {
                yield return property.Name;
            }
        }
    }
}