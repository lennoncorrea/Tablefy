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
        /// Takes an <see cref="IEnumerable{T}"/> of class models and parses
        /// into a <see cref="DataTable"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> items)
            where T : class
        {
            var dataTable = new DataTable();
            var props = typeof(T).GetProperties();
            var propsLength = props.Length;

            for (int i = 0; i < propsLength; i++)
            {
                var attribute = props[i].GetCustomAttribute<ColumnNameAttribute>();
                var column = new DataColumn(attribute?.ColumnName ?? props[i].Name,
                    props[i].PropertyType);
                dataTable.Columns.Add(column);
            }

            for (int i = 0; i < items.Count(); i++)
            {
                var propValues = new object[propsLength];
                for (int j = 0; j < propsLength; j++)
                {
                    propValues[j] = props[j].GetValue(items.ElementAt(i));
                }
                dataTable.Rows.Add(propValues);
            }
            return dataTable;
        }
    }
}

