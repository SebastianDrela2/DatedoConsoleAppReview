using ConsoleApp.Objects;

namespace ConsoleApp.ExtenstionMethods;

public static class DatabaseObjectExtenstionMethods
{
    public static bool IsChildOf(this IDatabaseObject databaseObject, IDatabaseObject parentDatabaseObject)
    {
        if (databaseObject.ParentType != parentDatabaseObject.Type)
        {
            return false;
        }

        if (databaseObject.ParentName != parentDatabaseObject.Name)
        {
            return false;
        }

        return true;
    }
}
