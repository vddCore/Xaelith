namespace Xaelith.DataModel.Storage;

using Xaelith.Common.Utilities;
using Xaelith.DataModel.Abstract.Storage;

public class TagDatabase : FlatFileDatabase<Tag>
{
    public Tag CreateNewTag(User author, string name)
    {
        var tag = new Tag
        {
            Id = Guid.NewGuid(),
            Ordinal = NextOrdinal++,
            Author = author,
            Name = name,
            CreatedDate = DateTime.Now,
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
        var tag = Find(name).FirstOrDefault();

        if (tag == null)
            return false;

        DeleteTag(tag);
        return true;
    }

    public void EditTag(Tag tag, Action<Tag> changes)
    {
        var prev = tag;
        
        Suppressed(() => Entries.Remove(tag));
        changes(tag);
        tag.LastEditDate = DateTime.Now;
        Suppressed(() => Entries.Add(tag));
        
        RaiseUpdatedEvent(new DatabaseUpdateEventArgs<Tag>(
            DatabaseUpdateAction.Edit,
            prev,
            tag
        ));
    }
}