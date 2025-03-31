namespace Xaelith.Services;

using Xaelith.DataModel.Composite;

public class StorageService
{
    public UserDatabase Users { get; } = new();
    public TagDatabase Tags { get; } = new();
    public CategoryDatabase Categories { get; } = new();
    public PageDatabase Pages { get; } = new();
    public PostDatabase Posts { get; } = new();

}