using System.Collections.Generic;

namespace ConsoleApp.Objects;

public interface IDatabaseObject
{
    public string Name { get; }
    public string DataType { get; }
    public string ParentName { get; }
    public string Schema { get; }
    public bool IsNullable { get; }  
    public DatabaseObjectType Type { get; }
    public DatabaseObjectType ParentType { get; }
    public IEnumerable<IDatabaseObject> Children { get; set; }    
}
