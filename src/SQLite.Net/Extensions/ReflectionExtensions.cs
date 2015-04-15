using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SQLite.Net.Extensions
{
    /// <summary>
    /// Extensions to use the existing code in Silverlght
    /// </summary>
    public static class ReflectionExtensions
    {
        public static IEnumerable<T> GetCustomAttributes<T>(this PropertyInfo prop, bool inherit = false)
        {
            if (prop == null)
            {
                throw new ArgumentNullException("prop", "Cannot be null");
            }

            var attr = prop.GetCustomAttributes(inherit)
                .Where(x => x.GetType() == typeof(T));

            //Create List
            var attributes = new List<T>();
            attributes.AddRange(attr.Select(x => (T)x));

            return attributes;
        }

        public static IEnumerable<T> GetCustomAttributes<T>(this Type type, bool inherit = false)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type", "Cannot be null");
            }

            var attr = type.GetCustomAttributes(inherit)
                .Where(x => x.GetType() == typeof(T));

            //Create List
            var attributes = new List<T>();
            attributes.AddRange(attr.Select(x => (T)x));

            return attributes;
        }

        public static IEnumerable<T> GetCustomAttributes<T>(this MemberInfo info, bool inherit = false)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info", "Cannot be null");
            }

            var attr = info.GetCustomAttributes(inherit)
                .Where(x => x.GetType() == typeof(T));

            //Create List
            var attributes = new List<T>();
            attributes.AddRange(attr.Select(x => (T)x));

            return attributes;
        }

        public static Object GetValue(this PropertyInfo p, Object obj)
        {
            return p.GetValue(obj, null);
        }

        public static PropertyInfo GetRuntimeProperty(this Type type, string name)
        {
            var prop = type.GetProperties().FirstOrDefault(x => x.Name == name);

            return prop;
        }
    }
}