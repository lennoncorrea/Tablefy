using System;
using Newtonsoft.Json;
using Tablefy.IEnumerableExtensions;
using Tablefy.Attributes;
using Tablefy.DataTableExtensions;

namespace Tablefy.Tests.IEnumerableExtensionsTests
{
    public class ToDataTableTests
    {
        [Fact]
        public void ToDataTableTest_ParseIEnumerableToDataTable()
        {
            List<ModelForTests> expectedItems = new();
            for (int i = 0; i < 10; i++)
            {
                expectedItems.Add(new ModelForTests
                {
                    NotId = i,
                    Name = $"test{i}",
                    Date = new DateTime(2000 + i, 1, 1)
                });
            }

            var table = expectedItems.ToDataTable();
            var items = table.To<ModelForTests>();

            Assert.Equal(JsonConvert.SerializeObject(expectedItems),
            JsonConvert.SerializeObject(items));
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

