using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using Tablefy.Attributes;

namespace Tablefy.IEnumerableExtensions
{
    public static partial class GenericIEnumerableExtensions
    {
        /// <summary>
        /// Converts a <see cref="IEnumerable{T}"/> into a CSV and get its <see cref="byte"/>s.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static byte[] ToCSV<T>(this IEnumerable<T> items) where T : class
        {
            var props = typeof(T).GetProperties();
            var propsLength = props.Length;
            var csv = new StringBuilder();

            for (int i = 0; i < propsLength; i++)
            {
                var attribute = props[i].GetCustomAttribute<ColumnNameAttribute>();
                var mappedPropName = attribute?.ColumnName ?? props[i].Name;
                csv.Append(i != propsLength - 1 ? $"{mappedPropName}," :
                    $"{mappedPropName}{Environment.NewLine}");
            }

            for (int i = 0; i < items.Count(); i++)
            {
                for (int j = 0; j < propsLength; j++)
                {
                    var propValue = props[j].GetValue(items.ElementAt(i));
                    csv.Append(j != propsLength - 1 ? $"{propValue}," :
                        $"{propValue}{Environment.NewLine}");
                }
            }
            return Encoding.ASCII.GetBytes(csv.ToString());
        }
    }
}

