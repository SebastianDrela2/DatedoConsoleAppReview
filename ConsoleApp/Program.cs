using ConsoleApp.Data;
using ConsoleApp.Objects;

namespace ConsoleApp;

internal class Program
{
    internal static void Main()
    {
        IDataReader reader = new DataReader(@"Data\data.csv");
        reader.ImportData();

        var databaseObjects = reader.GetDatabaseObjects();

        var databaseObjectsManager = new DatabaseObjectsManager(databaseObjects);
        databaseObjectsManager.SetChildren();

        var databases = databaseObjectsManager.GetDatabases();

        IDataPrinter printer = new DataPrinter(databases);
        printer.PrintData();
    }
}
