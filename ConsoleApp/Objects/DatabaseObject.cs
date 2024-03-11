namespace ConsoleApp.Objects;

public class DatabaseObject : DatabaseObjectBase, IDatabaseObject
{   
    public DatabaseObjectType ParentType { get; }
    public string DataType { get; }
    public string ParentName { get; }
    public string Schema { get; }
    public bool IsNullable { get; }         

    public DatabaseObject(string[] values) : base(values[0], values[1])
    {           
        Schema = GetCleanValue(values[2]);
        ParentName = GetCleanValue(values[3]);
        ParentType = GetDatabaseObjectType(values[4]);
        DataType = GetCleanValue(values[5]);

        if (values.Length > 6)
        {
            IsNullable = IsNullableValue(GetCleanValue(values[6]));
        }
    }
    
    private bool IsNullableValue(string value)
    {
        if (value is "1")
        {
            return true;
        }

        return false;
    }      
}   
