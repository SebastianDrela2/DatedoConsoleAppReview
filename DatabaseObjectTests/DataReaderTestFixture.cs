using NUnit.Framework;
using ConsoleApp.Data;

namespace DatabaseObjectTests;

[TestFixture]
internal class DataReaderTestFixture
{
    private IDataReader _reader;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _reader = new DataReader("data.csv");
    }

    [Test]
    public void ResultsAreCorrectlyFetched()
    {
        _reader.ImportData();
        var data = _reader.GetDatabaseObjects();

        Assert.That(data.Count, Is.EqualTo(1425));
    }

}
