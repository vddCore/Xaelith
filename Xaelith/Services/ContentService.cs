namespace Xaelith.Services;

using Markdig;
using Xaelith.Common;
using Xaelith.DataModel;
using Xaelith.DataModel.Configuration;
using Xaelith.DataModel.Storage;
using Xaelith.ServiceModel;

public class ContentService : IContentService
{
    private readonly Settings _settings;
    
    public ContentService(ConfigurationService configurationService)
    {
        _settings = configurationService.Settings!;
    }
    
    public DirectoryInfo GetContentDirectory(Post post)
        => new(Path.Combine(KnownPaths.Content, post.Id.ToString().ToLowerInvariant()));
    
    public DirectoryInfo GetContentDirectory(Page page)
        => new(Path.Combine(KnownPaths.Content, page.Id.ToString().ToLowerInvariant()));

    public string RenderContent(Post post)
    {
        var postDirectoryInfo = GetContentDirectory(post);

        if (!postDirectoryInfo.Exists)
            throw new MissingContentException(post.Id, $"Cannot find content directory for post {post.Id}.");

        var postContentFile = postDirectoryInfo.GetFiles(_settings.Core.ContentBodyFileName).FirstOrDefault();

        if (postContentFile == null)
            throw new MissingContentException(post.Id, $"Cannot find content body file for post {post.Id}.");

        using (var sr = postContentFile.OpenText())
            return Markdown.ToHtml(sr.ReadToEnd());
    }
    
    public void ReplaceContent(Post post, string newContent)
    {
        var postDirectoryInfo = GetContentDirectory(post);
        
        if (!postDirectoryInfo.Exists)
            postDirectoryInfo.Create();
        
        var postContentFile = postDirectoryInfo.GetFiles(_settings.Core.ContentBodyFileName).FirstOrDefault();
        
        if (postContentFile == null)
            postContentFile = new FileInfo(Path.Combine(postDirectoryInfo.FullName, _settings.Core.ContentBodyFileName));

        using (var sw = postContentFile.CreateText())
        {
            sw.AutoFlush = true;
            sw.Write(newContent);
        }
    }

    public string RenderContent(Page page)
    {
        var pageDirectoryInfo = GetContentDirectory(page);
        
        if (!pageDirectoryInfo.Exists)
            throw new MissingContentException(page.Id, $"Cannot find content directory for page {page.Id}.");
        
        var pageContentFile = pageDirectoryInfo.GetFiles(_settings.Core.ContentBodyFileName).FirstOrDefault();
        
        if (pageContentFile == null)
            throw new MissingContentException(page.Id, $"Cannot find content body file for page {page.Id}.");
        
        using (var sr = pageContentFile.OpenText())
            return Markdown.ToHtml(sr.ReadToEnd());
    }

    public void ReplaceContent(Page page, string newContent)
    {
        var pageDirectoryInfo = GetContentDirectory(page);
        
        if (!pageDirectoryInfo.Exists)
            pageDirectoryInfo.Create();
        
        var pageContentFile = pageDirectoryInfo.GetFiles(_settings.Core.ContentBodyFileName).FirstOrDefault();
        
        if (pageContentFile == null)
            pageContentFile = new FileInfo(Path.Combine(pageDirectoryInfo.FullName, _settings.Core.ContentBodyFileName));

        using (var sw = pageContentFile.CreateText())
        {
            sw.AutoFlush = true;
            sw.Write(newContent);
        }
    }
}