namespace Xaelith.DataModel.Abstract;

using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.Json.Serialization;

public abstract class FlatFileDatabase<T> where T : class, new()
{
    [JsonIgnore]
    public string? FilePath { get; set; }
    
    [JsonPropertyName("entries")]
    public ObservableCollection<T> Entries { get; set; } = [];

    public T? Find(Guid id)
    {
        if (!Entries.Any())
            return null;
        
        if (!typeof(T).IsAssignableTo(typeof(IIdentifiable)))
            throw new InvalidOperationException("This database does not support lookup by GUID.");
        
        return Entries.Single(x => (x as IIdentifiable)!.Id == id);
    }

    public virtual void Save()
    {
        if (FilePath == null)
        {
            throw new InvalidOperationException("File path is not set.");
        }
        
        using (var stream = new FileStream(FilePath, FileMode.Create, FileAccess.Write, FileShare.Read))
        {
            JsonSerializer.Serialize(stream, this);
        }
    }
}