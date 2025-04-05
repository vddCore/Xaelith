namespace Xaelith.DataModel.Abstract.Storage;

using System.Text.Json;
using System.Text.Json.Serialization;
using Xaelith.DataModel.Storage;

public abstract class FlatFileDatabase<T> 
    where T : class, IIdentifiable, new()
{
    public event EventHandler<DatabaseUpdateEventArgs<T>>? Updated; 
    
    [JsonIgnore]
    public string? FilePath { get; internal set; }
    
    [JsonPropertyName("next_ordinal")]
    public uint NextOrdinal { get; protected set; } = 0;
    
    [JsonPropertyName("entries")]
    public Dictionary<Guid, T> Entries { get; set; } = [];

    public bool Add(T entity)
    {
        if (Entries.ContainsKey(entity.Id))
            return false;
        
        Entries.Add(entity.Id, entity);
        return true;
    }

    public bool Remove(T entity)
        => Entries.Remove(entity.Id);

    public T? Find(Guid id)
    {
        if (Entries.TryGetValue(id, out var entry))
        {
            return entry;
        }
        
        return default(T?);
    }

    public IEnumerable<T> Find(string name)
    {
        if (!Entries.Any())
            return [];
            
        if (!typeof(T).IsAssignableTo(typeof(INameable)))
            throw new InvalidOperationException("This database does not support lookup by name.");
        
        return Entries.Values.Where(x => ((INameable)x).Name == name);
    }

    public virtual void Save()
    {
        if (FilePath == null)
            throw new InvalidOperationException("File path is not set.");
        
        using (var stream = new FileStream(FilePath, FileMode.Create, FileAccess.Write, FileShare.Read))
            JsonSerializer.Serialize(stream, this);
    }

    protected void RaiseUpdatedEvent(DatabaseUpdateEventArgs<T> e)
        => Updated?.Invoke(this, e);
}