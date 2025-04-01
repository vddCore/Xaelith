namespace Xaelith.Services;

using System.Text.Json;
using Xaelith.Common;
using Xaelith.DataModel;
using Xaelith.DataModel.Abstract;
using Xaelith.DataModel.Configuration;
using Xaelith.DataModel.Storage;
using Xaelith.ServiceModel;

public class StorageService : IStorageService
{
    private Settings _settings;
    
    public UserDatabase Users { get; }
    public TagDatabase Tags { get; }
    public CategoryDatabase Categories { get; }
    public PageDatabase Pages { get; }
    public PostDatabase Posts { get; }

    public StorageService(IConfigurationService configurationService)
    {
        _settings = configurationService.Settings;

        Users = Load<User, UserDatabase>(_settings.Core.UserDatabase);
        Tags = Load<Tag, TagDatabase>(_settings.Core.TagDatabase);
        Categories = Load<Category, CategoryDatabase>(_settings.Core.CategoryDatabase);
        Pages = Load<Page, PageDatabase>(_settings.Core.PageDatabase);
        Posts = Load<Post, PostDatabase>(_settings.Core.PostDatabase);
    }
    
    private U Load<T, U>(string fileName, bool createOnFailure = true)
        where T : class, new()
        where U : FlatFileDatabase<T>, new()
    {
        FlatFileDatabase<T>? db = null;

        var filePath = Path.Combine(KnownPaths.Databases, fileName);

        using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            db = JsonSerializer.Deserialize<FlatFileDatabase<T>>(stream);
        }

        if (db == null)
        {
            if (!createOnFailure)
            {
                throw new IOException($"Unable to load database from file '{filePath}'.");
            }
            
            db = new U();
        }

        db.FilePath = filePath;
        db.Save();
        
        return (U)db;
    }
}