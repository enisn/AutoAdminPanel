using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoAdmin.Mvc.Helpers
{

    //class user
    //{
    //    public int id { get; set; }
    //    public string Test { get; set; }
    //    public userdetail detail { get; set; }
    //    public location position { get; set; }
    //    public ICollection<categories> Categories { get; set; }
    //    public ICollection<car> Cars { get; set; }
    //}
    //class userdetail
    //{
    //    public int id { get; set; }
    //    public string something { get; set; }
    //    public user user { get; set; }
    //}
    //class car
    //{
    //    public int id { get; set; }
    //    public string somethingname { get; set; }
    //    public user owner { get; set; }
    //}
    //class location
    //{
    //    public int id { get; set; }
    //    public double x { get; set; }
    //    public double y { get; set; }
    //    public ICollection<user> users { get; set; }
    //}
    //class categories
    //{
    //    public int id { get; set; }
    //    public string something { get; set; }
    //    public ICollection<user> users { get; set; }
    //}
    public static class RelationHelper
    {
        // root = userdetail              | with = userdetail   | OneToOne
        // root = ICollection<car>        | with = car          | OneToMany
        // root = location                | with = location     | ManyToOne
        // root = ICollection<categories> | with = categories   | ManyToMany
        /// <summary>
        /// Finds relations between tables
        /// </summary>
        /// <param name="from">this must be one of property of Entity</param>
        /// <param name="to">this is the relation entity</param>
        /// <returns></returns>
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
            //var from = isCollection ? property.PropertyType.GetGenericArguments()[0] : property.PropertyType;
            foreach (var targetProperty in properties)
            {
                if (targetProperty.PropertyType == property.DeclaringType.BaseType)                
                    return isCollection ? Relation.OneToMany : Relation.OneToOne;
                //try
                //{

                //    var t1 = targetProperty.PropertyType.GetGenericArguments()[0];
                //    var d1 = property.DeclaringType;
                //    var t2 = targetProperty.PropertyType.GetGenericParameterConstraints();
                //    var t3 = targetProperty.PropertyType.GetGenericTypeDefinition();
                //}
                //catch (Exception ex)
                //{
                //}

                if (targetProperty.PropertyType.IsConstructedGenericType && targetProperty.PropertyType.GetGenericArguments()[0] == property.DeclaringType.BaseType)                
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
