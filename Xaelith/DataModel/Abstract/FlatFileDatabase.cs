namespace Xaelith.DataModel.Abstract;

using System.Text.Json;
using System.Text.Json.Serialization;

public abstract class FlatFileDatabase<T> where T : class
{
    [JsonIgnore]
    public string FilePath { get; private set; } = string.Empty;
    
    [JsonPropertyName("entries")]
    public List<T> Entries { get; set; } = [];
    
    public T? Find(Guid id)
    {
        if (!Entries.Any())
            return null;
        
        if (!typeof(T).IsAssignableTo(typeof(IIdentifiable)))
            throw new InvalidOperationException("This database does not support lookup by GUID.");
        
        return Entries.Single(x => (x as IIdentifiable)!.Id == id);
    }

    public virtual async Task SaveAsync()
    {
        using (var stream = new FileStream(FilePath, FileMode.Create, FileAccess.Write, FileShare.Read))
        {
            await JsonSerializer.SerializeAsync(stream, this);
        }
    }

    public static async Task<FlatFileDatabase<T>> LoadAsync(string filePath)
    {
        using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            var db = await JsonSerializer.DeserializeAsync<FlatFileDatabase<T>>(stream)
                     ?? throw new IOException($"Unable to load database from file '{filePath}'.");
            
            db.FilePath = filePath;

            return db;
        }
    }
}