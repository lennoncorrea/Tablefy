using System.Data;
using System.Dynamic;
using Newtonsoft.Json;
using Tablefy.Attributes;
using Tablefy.DataTableExtensions;

namespace Tablefy.Tests.DataTableExtensionsTests;

public class ToTests
{
    [Fact]
    public void To_ValidModel()
    {
        DataTable dataTable = new();
        DataColumn idColumn = new("Id", typeof(int));
        DataColumn nameColumn = new("Name", typeof(string));
        DataColumn dateColumn = new("Date", typeof(DateTime));

        dataTable.Columns.Add(idColumn);
        dataTable.Columns.Add(nameColumn);
        dataTable.Columns.Add(dateColumn);

        for (int i = 0; i < 10; i++)
        {
            dataTable.Rows.Add(new object[] {
                i, $"test{i}", new DateTime(2000+i, 1, 1)
            });
        }

        var objs = dataTable.To<ModelForTests>();

        List<ModelForTests> expectedObjs = new();
        for (int i = 0; i < 10; i++)
        {
            expectedObjs.Add(new ModelForTests
            {
                NotId = i,
                Name = $"test{i}",
                Date = new DateTime(2000 + i, 1, 1)
            });
        }

        Assert.Equal(JsonConvert.SerializeObject(expectedObjs),
            JsonConvert.SerializeObject(objs));
    }

    [Fact]
    public void To_InvalidModel()
    {
        DataTable dataTable = new();
        DataColumn idColumn = new("Id", typeof(int));
        DataColumn nameColumn = new("Name", typeof(string));

        dataTable.Columns.Add(idColumn);
        dataTable.Columns.Add(nameColumn);

        for (int i = 0; i < 10; i++)
        {
            dataTable.Rows.Add(new object[] {
                i, $"test{i}"
            });
        }

        Assert.Throws<ArgumentException>(dataTable.To<ModelForTests>().ToList);
    }

    private class ModelForTests
    {
        [ColumnName("Id")]
        public int NotId { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }
    }
}
