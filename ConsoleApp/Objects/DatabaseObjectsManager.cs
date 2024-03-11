using ConsoleApp.ExtenstionMethods;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp.Objects;

public class DatabaseObjectsManager : IDatabaseObjectsManager
{
    private readonly IEnumerable<IDatabaseObject> _databases;
    private readonly IEnumerable<IDatabaseObject> _tables;
    private readonly IEnumerable<IDatabaseObject> _columns;

    public DatabaseObjectsManager(IEnumerable<IDatabaseObject> databaseObjects)
    {
        _databases = databaseObjects.Where(x => x.Type is DatabaseObjectType.Database);
        _tables = databaseObjects.Where(x => x.Type is DatabaseObjectType.Table);
        _columns = databaseObjects.Where(x => x.Type is DatabaseObjectType.Column);
    }

    public void SetChildren()
    {                   
        foreach (var database in _databases)
        {
            var tables = _tables.Where(x => x.IsChildOf(database));

            database.Children = tables;
            SetTablesChildren(tables);
        }
    }

    public IEnumerable<IDatabaseObject> GetDatabases()
    {
        return _databases;
    }

    private void SetTablesChildren(IEnumerable<IDatabaseObject> tables)
    {
        foreach (var table in tables)
        {
            var columns = _columns.Where(x => x.IsChildOf(table));
            table.Children = columns;
        }
    }
}
