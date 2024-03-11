## Code Review:

So in the summary, proposed codebase is currently unacceptable there will be a lot of rewriting involved in order to make this production friendly.


### Usage Issues


- Path to the CSV is invalid should be `data.csv` instead of `dataa.csv`
- Why `ImportedObjects` field is initialized with an object already inside it? This will cause data to be inconsistent.
- First `for` loop in the `DataReader.cs` iterates one more time than it should, specifiy iteration requirement to be less than rather than less than or equals.
- Some lines in the .csv are empty, upon runtime crash occurs make sure to properly check empty,null values.
- Ocassionally read line does not have `IsNullable` specified, this causes a runtime error, make sure to check that the list is actually longer than 6 before asigning that property.

### Refactoring


- There is no need to keep fields public if they are not used outside of the class scope make them private and readonly if possible, also rename `ImportedObject.cs` to `DatabaseObject.cs` for e.x
we should point that this class is related to relational databases.
- Use properties instead of public fields for the objects, this will make it easier to test made changes later for most of them just getter is enough, 
also properly case them (match C# standards such as snakeCase)
- Unneccesary optional parameter is specified in the `ImportAndPrintData` func, you can remove it.
- `Name` property in the `ImportedObject.cs` hides inherited property from the base class, you can remove it.
- Base class `ImportedObjectBaseClass.cs` should be specified as abstract we dont make object out of it, also there is no need to specify that it is a "Class"
- Try to keep objects casted to an interface, libraries such as Nsubstitute or NUnit heavily benefit on this.
- Split datareader into a datareader and a dataprinter, this violates SOLID principles otherwise because a `DataReader` does importing and printing rather than just importing.
- Remove unused using statements, seperate classes inside DataReader into seperate files.
- Use bool value for `IsNullable` property and customType for declaring a `Type` and `ParentType`, 
we usually dont store types in strings, make sure to store enum in a seperate file.
- Overall seperate certain parts of code into seperate functions, function ideally should not be longer than 20-30 lines of code
- Ideally avoid having functions which have nested if statmements exceeding 2-3 levels, making an extenstion method helps.
- Remove `NumberOfChildren` property from `ImportedObject.cs` and instead asign children manually in a seperate object, 
put a collection of Children in a base class, 
this will make it both flexible for data reader and data printer.
- Please use the new .SDK project format, the old one is outdated, also since this is a simple console application you can easily update it to the newest .NET 8 no need to use .NET Framework.
- Git ignore is bloated now with a lot of unneccesary stuff simplify it as well.
- Make sure to always specify access modifier for a a field,property,function,class while it is a good practice to know what is a default access modifier for each one its way more readable that way.
- In this case you can change `for` loop to `foreach` performance loss is miniscule.
- `ImportedObjects` field in `DataReader.cs` can just be type of IList then cast at line 39 is redundant, we dont get to experience here power of IEnumerable enumarator yield return etc.
- Unneccesary comments object, class, function name should describe their purpose, simple one line comments are only used to explain why certain part of code is there or point to a future action.

### Testing


- Changes remained untested, there seems to be neither automation or integration involved. 
So simple unit tests in Nunit should be enough
