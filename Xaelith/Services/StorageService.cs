namespace Xaelith.Services;

using Xaelith.DataModel.Storage;
using Xaelith.ServiceModel;

public class StorageService : IStorageService
{
    public UserDatabase Users { get; } = new();
    public TagDatabase Tags { get; } = new();
    public CategoryDatabase Categories { get; } = new();
    public PageDatabase Pages { get; } = new();
    public PostDatabase Posts { get; } = new();

}