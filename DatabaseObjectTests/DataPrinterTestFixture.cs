using ConsoleApp.Data;
using ConsoleApp.Objects;
using NUnit.Framework;
using System.Reflection;

namespace DatabaseObjectTests;

[TestFixture]
internal class DataPrinterTestFixture
{       
    private IDataPrinter _printer;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var reader = new DataReader("data.csv");
        reader.ImportData();

        var databaseObjects = reader.GetDatabaseObjects();

        var databaseObjectsManager = new DatabaseObjectsManager(databaseObjects);
        databaseObjectsManager.SetChildren();
        var databases = databaseObjectsManager.GetDatabases();

        _printer = new DataPrinter(databases);
    }

    [Test]
    public void DataIsCorrectlyDisplayed()
    {
        var tempFilePath = Path.Combine(Path.GetTempPath(), "tempPrinterData.txt");

        try
        {
            using var streamWriter = new StreamWriter(tempFilePath);
            Console.SetOut(streamWriter);

            _printer.PrintData();
            streamWriter.Close();

            var printedData = File.ReadAllText(tempFilePath);
            var expectedData = ReadResource("DatabaseObjectTests.expectedData.txt");

            Assert.That(printedData, Is.EqualTo(expectedData));
        }
        finally
        {
            File.Delete(tempFilePath);
        }
    }

    private string ReadResource(string resourceName)
    {
        var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);            

        using var reader = new StreamReader(stream!);
        var resource = reader.ReadToEnd();

        return resource;
    }
}
