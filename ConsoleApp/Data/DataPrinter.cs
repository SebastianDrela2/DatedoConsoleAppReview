using ConsoleApp.ExtenstionMethods;
using ConsoleApp.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp.Data;

public class DataPrinter : IDataPrinter
{
    private readonly IEnumerable<IDatabaseObject> _databases;

    public DataPrinter(IEnumerable<IDatabaseObject> databases)
    {
        _databases = databases;
    }

    public void PrintData()
    {           
        foreach (var database in _databases)
        {
            Console.WriteLine($"Database '{database.Name}' ({database.Children.Count()} tables)");

            PrintTables(database);
        }
    }

    private void PrintTables(IDatabaseObject database)
    {           
        foreach (var table in database.Children)
        {
            Console.WriteLine($"\tTable '{table.Schema}.{table.Name}' ({table.Children.Count()} columns)");

            PrintColumns(table);
        }
    }

    private void PrintColumns(IDatabaseObject table)
    {          
        foreach (var column in table.Children)
        {
            Console.WriteLine($"\t\tColumn '{column.Name}' with {column.DataType} data type {(column.IsNullable ? "accepts nulls" : "with no nulls")}");
        }
    }
}
