using ConsoleApp.Objects;
using System.Collections.Generic;

namespace ConsoleApp.Data;

public interface IDataReader
{
    public void ImportData();
    public IEnumerable<IDatabaseObject> GetDatabaseObjects();       
}
