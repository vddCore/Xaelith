namespace Xaelith.ServiceModel;

using Xaelith.DataModel;

public interface IContentService
{
    DirectoryInfo GetContentDirectory(Post post);
    DirectoryInfo GetContentDirectory(Page page);
    
    string RenderContent(Post post);
    void ReplaceContent(Post post, string newContent);
    
    string RenderContent(Page page);
    void ReplaceContent(Page page, string newContent);
}