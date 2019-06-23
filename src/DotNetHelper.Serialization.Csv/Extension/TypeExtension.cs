using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace DotNetHelper.Serialization.Csv.Extension
{
    internal static class TypeExtension
    {


        public static Type GetEnumerableItemType(this Type type)
        {
            if (type.IsArray)
            {
                return type.GetElementType();
            }

            Type elementType = null;

            if (type == typeof(IEnumerable))
            {
                elementType = typeof(object);
            }
            else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                elementType = type.GetGenericArguments()[0];
            }
            else
            {
                foreach (var interfaceType in type.GetInterfaces())
                {
                    if (interfaceType == typeof(IEnumerable))
                    {
                        elementType = typeof(object);
                    }
                    else if (interfaceType.IsGenericType &&
                             interfaceType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                    {
                        elementType = interfaceType.GetGenericArguments()[0];
                        break;
                    }
                }
            }


            return elementType;
        }


    }
}
