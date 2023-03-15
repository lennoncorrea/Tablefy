using System;
using System.Data;
using System.Text;
using Tablefy.Attributes;

namespace Tablefy.DataTableExtensions
{
    public static partial class DataTableExtensions
    {
        /// <summary>
        /// Converts a <see cref="DataTable"/> into a CSV and get its
        /// <see cref="byte"/>s.
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static byte[] ToCSV(this DataTable dataTable)
        {
            var columsLength = dataTable.Columns.Count;
            var rowsLength = dataTable.Rows.Count;
            var csv = new StringBuilder();

            for (int i = 0; i < columsLength; i++)
            {
                var columnName = dataTable.Columns[i].ColumnName;
                csv.Append(i != columsLength - 1 ? $"{columnName}," :
                    $"{columnName}{Environment.NewLine}");
            }

            for (int i = 0; i < rowsLength; i++)
            {
                for (int j = 0; j < columsLength; j++)
                {
                    var value = dataTable.Rows[i].Field<object>(dataTable.Columns[j]);
                    csv.Append(j != columsLength - 1 ? $"{value}," :
                        $"{value}");
                }
                csv.Append(Environment.NewLine);
            }
            return Encoding.ASCII.GetBytes(csv.ToString());
        }
    }
}
