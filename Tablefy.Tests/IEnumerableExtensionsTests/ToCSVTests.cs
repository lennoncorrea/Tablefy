using System;
using System.Collections;
using System.Text;
using Tablefy.Attributes;
using Tablefy.IEnumerableExtensions;

namespace Tablefy.Tests.IEnumerableExtensionsTests
{
    public class ToCSVTests
    {
        [Fact]
        public void ToCSV_ParseIEnumerableToCSVBytes()
        {
            List<ModelForTests> items = new();
            for (int i = 0; i < 3; i++)
            {
                items.Add(new ModelForTests
                {
                    NotId = i,
                    Name = $"test{i}",
                    Date = new DateTime(2000 + i, 1, 1)
                });
            }

            var csvBytes = items.ToCSV();
            var csv = Encoding.UTF8.GetString(csvBytes);

            var expectedCsv = $"Id,Name,Date\n" +
                $"0,test0,01/01/2000 00:00:00\n" +
                $"1,test1,01/01/2001 00:00:00\n" +
                $"2,test2,01/01/2002 00:00:00\n";

            Assert.Equal(csv, expectedCsv);
        }

        private class ModelForTests
        {
            [ColumnName("Id")]
            public int NotId { get; set; }

            public string Name { get; set; }

            public DateTime Date { get; set; }
        }
    }
}

