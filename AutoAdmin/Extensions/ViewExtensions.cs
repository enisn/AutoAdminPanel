using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoAdmin.Extensions
{
    public static class ViewExtensions
    {
        public static SelectList ToSelectList(this IEnumerable collection, object selected = null)
        {
            var enumerator = collection.GetEnumerator();
            enumerator.MoveNext();
            var first = enumerator.Current;
            if (first != null)
                return new SelectList(collection, selected);
            else
                return new SelectList(new[] { "Veri Bulunamadı!" });
        }

        public static SelectList ToSelectList(this object collection, object selected = null)
        {
            if (collection is IEnumerable)
                return ToSelectList(collection as IEnumerable, selected);

            throw new InvalidCastException("Data type is not a valid IEnumerable");
        }
    }
}