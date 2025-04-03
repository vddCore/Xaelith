namespace Xaelith.DataModel.Storage;

public class DuplicateNameException : Exception
{
    public string Name { get; }
    
    public DuplicateNameException(string name, string message) : base(message)
    {
        Name = name;
    }
}