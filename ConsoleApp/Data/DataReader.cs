using ConsoleApp.Objects;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp.Data;

public class DataReader : IDataReader
{
    private readonly IList<IDatabaseObject> _databaseObjects;
    private readonly string _fileName;

    public DataReader(string fileName)
    {
        _databaseObjects = new List<IDatabaseObject>();
        _fileName = fileName;
    }

    public void ImportData()
    {            
        var streamReader = new StreamReader(_fileName);
        var importedLines = GetNonEmptyImportedLines(streamReader);
        
        foreach (var importedLine in importedLines)
        {                
            var lineValues = importedLine.Split(';');                             
            var databaseObject = new DatabaseObject(lineValues);

            _databaseObjects.Add(databaseObject);              
        }                                                                                  
    }

    private IList<string> GetNonEmptyImportedLines(StreamReader reader)
    {
        var importedLines = new List<string>();

        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();

            if (!string.IsNullOrEmpty(line))
            {
                importedLines.Add(line);
            }
        }

        return importedLines;
    }

    public IEnumerable<IDatabaseObject> GetDatabaseObjects() => _databaseObjects;               
}         
