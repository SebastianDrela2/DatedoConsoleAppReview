using ConsoleApp.Data;
using ConsoleApp.Objects;
using NUnit.Framework;

namespace DatabaseObjectTests;

[TestFixture]
internal class DatabaseObjectsTestFixture
{
    private IDatabaseObjectsManager _databaseObjectsManager;
    private IEnumerable<IDatabaseObject> _databaseObjects;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var reader = new DataReader("data.csv");
        reader.ImportData();

        _databaseObjects = reader.GetDatabaseObjects();

        _databaseObjectsManager = new DatabaseObjectsManager(_databaseObjects);
        _databaseObjectsManager.SetChildren();
    }

    [Test]
    public void DatabaseIsCreatedCorrectly()
    {
        var values = new string[] {"Database", "AdventureWorks2022", "", "", "", "" };

        var databaseObject = new DatabaseObject(values);

        Assert.That(databaseObject.Type == DatabaseObjectType.Database);
        Assert.That(databaseObject.Name == values[1]);                                     
    }

    [Test]
    public void TableIsCreatedCorrectly()
    {
        var values = new string[] { "Table", "Table1", "dbo", "AdventureWorks2022", "Database", "" };

        var databaseObject = new DatabaseObject(values);

        Assert.That(databaseObject.Type == DatabaseObjectType.Table);
        Assert.That(databaseObject.Name == values[1]);
        Assert.That(databaseObject.Schema == values[2]);
        Assert.That(databaseObject.ParentName == values[3]);
        Assert.That(databaseObject.ParentType == DatabaseObjectType.Database);
    }

    [Test]
    public void ColumnIsCreatedCorrectly()
    {
        var values = new string[] { "Column", "Column1", "", "Table1", "Table", "int", "1"};

        var databaseObject = new DatabaseObject(values);

        Assert.That(databaseObject.Type == DatabaseObjectType.Column);
        Assert.That(databaseObject.Name == values[1]);
        Assert.That(databaseObject.Schema == values[2]);           
        Assert.That(databaseObject.ParentName == values[3]);
        Assert.That(databaseObject.ParentType == DatabaseObjectType.Table);
        Assert.That(databaseObject.DataType == "int");
        Assert.That(databaseObject.IsNullable);
    }

    [Test]
    public void DatabaseObjectsManagerCorrectlySetsChildren()
    {        
        var databases = _databaseObjects.Where(x => x.Type is DatabaseObjectType.Database).ToList();

        Assert.That(databases[0].Children.Count() is 92);
        Assert.That(databases[1].Children.Count() is 0);
        Assert.That(databases[2].Children.Count() is 25);
        Assert.That(databases[3].Children.Count() is 0);
        Assert.That(databases[4].Children.Count() is 13);
    }

    [Test]
    public void DatabaseObjectsManagerGetsDatabasesCorrectly()
    {
        var databases = _databaseObjectsManager.GetDatabases();

        Assert.That(databases.All(x => x.Type is DatabaseObjectType.Database));
        Assert.That(databases.Count() is 5);
    }
}