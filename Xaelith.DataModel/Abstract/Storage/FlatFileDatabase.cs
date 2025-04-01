namespace Xaelith.DataModel.Abstract.Storage;

using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xaelith.DataModel.Storage;

public abstract class FlatFileDatabase<T> 
    where T : class, new()
{
    public event EventHandler<DatabaseUpdateEventArgs<T>>? Updated; 
    
    [JsonIgnore]
    public string? FilePath { get; internal set; }
    
    [JsonIgnore]
    internal bool SuppressEvents { get; set; }
    
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

    public IEnumerable<T> Find(string name)
    {
        if (!Entries.Any())
            return [];
            
        if (!typeof(T).IsAssignableTo(typeof(INameable)))
            throw new InvalidOperationException("This database does not support lookup by name.");
        
        return Entries.Where(x => (x as INameable)?.Name == name);
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

    protected void RaiseUpdatedEvent(DatabaseUpdateEventArgs<T> e)
        => Updated?.Invoke(this, e);
}