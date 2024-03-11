using System;
using System.Collections.Generic;

namespace ConsoleApp.Objects;

public abstract class DatabaseObjectBase
{
    public DatabaseObjectType Type { get; }
    public IEnumerable<IDatabaseObject> Children { get; set; }
    public string Name { get; }

    public DatabaseObjectBase(string typeName, string name)
    {
        Type = GetDatabaseObjectType(typeName);
        Name = GetCleanValue(name);           
    }    
    
    protected string GetCleanValue(string input) => input.Trim().Replace(" ", "").Replace(Environment.NewLine, "");

    protected DatabaseObjectType GetDatabaseObjectType(string typeName)
    {
        var cleanTypeName = GetCleanValue(typeName).ToUpper();

        return cleanTypeName.ToUpper() switch
        {
            "DATABASE" => DatabaseObjectType.Database,
            "TABLE" => DatabaseObjectType.Table,
            "COLUMN" => DatabaseObjectType.Column,
            _ => DatabaseObjectType.InvalidObject //we should log this in the future.
        };
    }
}
