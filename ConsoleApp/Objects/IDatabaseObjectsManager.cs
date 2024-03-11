using System.Collections.Generic;

namespace ConsoleApp.Objects
{
    public interface IDatabaseObjectsManager
    {
        public void SetChildren();
        public IEnumerable<IDatabaseObject> GetDatabases();
    }
}