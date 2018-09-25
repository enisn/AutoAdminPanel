using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AutoAdmin.Mvc.Core.Helpers
{
    public static class RelationHelper
    {
        public static Relation RelatedWith(this Type from, Type to)
        {

            var rootIsGeneric = from.IsGenericType;
            if (rootIsGeneric)
                from = from.GetGenericArguments()[0];
            foreach (var property in to.GetProperties())
            {

                if (property.PropertyType == from)
                    return rootIsGeneric ? Relation.OneToMany : Relation.OneToOne;

                if (property.PropertyType == (from.IsGenericType ? from.GetGenericArguments()[0] : null))
                    return rootIsGeneric ? Relation.ManyToMany : Relation.ManyToOne;
            }
            return Relation.None;
        }

        public static Relation GetRelation(this PropertyInfo property)
        {
            var propertyType = property.PropertyType;
            if (propertyType.IsArray || propertyType == typeof(string) || (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                return Relation.None;

            bool isCollection = propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(ICollection<>);
            var properties = isCollection ? propertyType.GetGenericArguments()[0].GetProperties() : propertyType.GetProperties();

            foreach (var targetProperty in properties)
            {
                if (targetProperty.PropertyType == property.DeclaringType.BaseType || targetProperty.PropertyType == property.DeclaringType)
                    return isCollection ? Relation.OneToMany : Relation.OneToOne;

                if (targetProperty.PropertyType.IsConstructedGenericType && (targetProperty.PropertyType.GetGenericArguments()[0] == property.DeclaringType.BaseType || targetProperty.PropertyType.GetGenericArguments()[0] == property.DeclaringType))
                    return isCollection ? Relation.ManyToMany : Relation.ManyToOne;
            }
            return Relation.None;
        }
    }

    [Flags]
    public enum Relation
    {
        None,
        OneToOne,
        OneToMany,
        ManyToOne,
        ManyToMany
    }
}

