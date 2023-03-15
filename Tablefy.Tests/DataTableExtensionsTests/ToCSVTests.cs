using System;
using System.Data;
using System.Text;
using Tablefy.DataTableExtensions;
using Xunit.Abstractions;

namespace Tablefy.Tests.DataTableExtensionsTests
{
    public class ToCSVTests
    {
        [Fact]
        public void ToCSV_ConvertsDatableIntoCSV()
        {
            DataTable dataTable = new();
            DataColumn idColumn = new("Id", typeof(int));
            DataColumn nameColumn = new("Name", typeof(string));
            DataColumn dateColumn = new("Date", typeof(DateTime));

            dataTable.Columns.Add(idColumn);
            dataTable.Columns.Add(nameColumn);
            dataTable.Columns.Add(dateColumn);

            for (int i = 0; i < 3; i++)
            {
                dataTable.Rows.Add(new object[] {
                    i, $"test{i}", new DateTime(2000+i, 1, 1)
                });
            }

            var csvBytes = dataTable.ToCSV();
            var csv = Encoding.UTF8.GetString(csvBytes);

            var expectedCsv = $"Id,Name,Date\n" +
                $"0,test0,01/01/2000 00:00:00\n" +
                $"1,test1,01/01/2001 00:00:00\n" +
                $"2,test2,01/01/2002 00:00:00\n";

            Assert.Equal(csv, expectedCsv);
        }
    }
}
