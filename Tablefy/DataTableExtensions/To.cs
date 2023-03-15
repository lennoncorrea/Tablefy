using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Tablefy.Attributes;

namespace Tablefy.DataTableExtensions
{
    public static partial class DataTableExtensions
    {
        /// <summary>
        /// Converts each <see cref="DataRow"/> into a <typeparamref name="T"/>
        /// object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns>A <see cref="IEnumerable{T}"/> of parsed rows
        /// <typeparamref name="T"/> objects.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static IEnumerable<T> To<T>(this DataTable table) where T : class
        {
            var props = typeof(T).GetProperties();
            var propsLength = props.Length;
            var propsNames = new string[propsLength];

            for (int i = 0; i < propsLength; i++)
            {
                var attribute = props[i].GetCustomAttribute<ColumnNameAttribute>();
                propsNames[i] = attribute?.ColumnName ?? props[i].Name;
            }

            for (int i = 0; i < table.Rows.Count; i++)
            {
                var obj = Activator.CreateInstance<T>();
                for (int j = 0; j < propsLength; j++)
                {
                    props[j].SetValue(obj, table.Rows[i][propsNames[j]]);

                }
                yield return obj;
            }
        }
    }
}

