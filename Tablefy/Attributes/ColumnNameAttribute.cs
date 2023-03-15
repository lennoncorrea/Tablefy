using System;

namespace Tablefy.Attributes
{

    /// <summary>
    /// Allows to specify the column name to be mapped in a property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property,
        AllowMultiple = false, Inherited = false)]
    public class ColumnNameAttribute : Attribute
    {
        public string ColumnName { get; set; }
        public ColumnNameAttribute(string columnName)
        {
            ColumnName = columnName;
        }
    }
}

