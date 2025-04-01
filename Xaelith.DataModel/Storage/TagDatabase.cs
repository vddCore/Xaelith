namespace Xaelith.DataModel.Storage;

using Xaelith.Common.Utilities;
using Xaelith.DataModel.Abstract.Storage;

public class TagDatabase : FlatFileDatabase<Tag>
{
    public Tag CreateNewTag(User author, string name)
    {
        var tag = new Tag
        {
            CreatedDate = DateTime.Now,
            Author = author,
            Name = name,
            Slug = UrlUtilities.GenerateSlug(name)
        };
        
        Entries.Add(tag);

        RaiseUpdatedEvent(new DatabaseUpdateEventArgs<Tag>(
            DatabaseUpdateAction.Add,
            null,
            tag
        ));
        
        return tag;
    }

    public bool DeleteTag(Tag tag)
    {
        if (Entries.Remove(tag))
        {
            RaiseUpdatedEvent(new DatabaseUpdateEventArgs<Tag>(
                DatabaseUpdateAction.Delete,
                tag,
                null
            ));
            
            return true;
        }

        return false;
    }

    public bool DeleteTag(string name)
    {
        var tag = Entries.FirstOrDefault(x => x.Name == name);

        if (tag == null)
        {
            return false;
        }

        DeleteTag(tag);
        return true;
    }

    public void EditTag(Tag tag, Action<Tag> changes)
    {
        var prev = tag;
        
        Entries.Remove(tag);
        {
            changes(tag);
            tag.LastEditDate = DateTime.Now;
            
            RaiseUpdatedEvent(new DatabaseUpdateEventArgs<Tag>(
                DatabaseUpdateAction.Edit,
                prev,
                tag
            ));
        }
        Entries.Add(tag);
    }
}