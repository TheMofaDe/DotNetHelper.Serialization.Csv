using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using DotNetHelper.Serialization.Csv.Extension;

namespace DotNetHelper.Serialization.Json.Extension
{
    internal static class ObjectExtension
    {
        internal static void IsNullThrow(this object obj, string name, Exception error = null)
        {
            if (obj != null) return;
            if (error == null) error = new ArgumentNullException(name);
            throw error;
        }
        /// <summary>
        /// Obtains the data as a list; if it is *already* a list, the original object is returned without
        /// any duplication; otherwise, ToList() is invoked.
        /// </summary>
        /// <typeparam name="T">The type of element in the list.</typeparam>
        /// <param name="source">The enumerable to return as a list.</param>
        public static List<T> AsList<T>(this IEnumerable<T> source) => (source == null || source is List<T>) ? (List<T>)source : source.ToList();


        /// <summary>
        /// https://stackoverflow.com/questions/22939552/convert-listobject-to-listtype-type-is-known-at-runtime
        /// </summary>
        /// <param name="items"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object ConvertListToTypeList(this IEnumerable<object> items, Type type)
        {
            if (type.GetEnumerableItemType().IsTypeDynamic()) return items;

            var genericTypeArguments = type.GenericTypeArguments;
            var containedType = genericTypeArguments.FirstOrDefault() ?? type;
            var enumerableType = typeof(Enumerable);
            var castMethod = enumerableType.GetMethod(nameof(Enumerable.Cast)).MakeGenericMethod(containedType);
            var toListMethod = enumerableType.GetMethod(nameof(Enumerable.ToList)).MakeGenericMethod(containedType);
            var itemsToCast = items.Select(item => Convert.ChangeType(item, containedType));
            var castedItems = castMethod.Invoke(null, new[] { itemsToCast });
            return toListMethod.Invoke(null, new[] { castedItems });

            return items;
        }


        /// <summary>
        /// return true if the type is assignable form IDynamicMetaObjectProvider
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsTypeDynamic(this Type type)
        {
            return typeof(IDynamicMetaObjectProvider).IsAssignableFrom(type);
        }

    }
}
