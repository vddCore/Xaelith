namespace Xaelith.ServiceModel;

using Xaelith.DataModel.Storage;

public interface IStorageService
{
    UserDatabase Users { get; }
    TagDatabase Tags { get; }
    CategoryDatabase Categories { get; }
    PageDatabase Pages { get; }
    PostDatabase Posts { get; }
}