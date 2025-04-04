namespace Xaelith.DataModel.Storage;

using Xaelith.Common.Utilities;
using Xaelith.DataModel.Abstract.Storage;

public class TagDatabase : FlatFileDatabase<Tag>
{
    public Tag? CreateNewTag(User author, string name)
    {
        var tag = new Tag
        {
            Id = Guid.NewGuid(),
            Ordinal = NextOrdinal++,
            AuthorUserId = author.Id,
            Name = name,
            CreatedDate = DateTime.Now,
            Slug = UrlUtilities.GenerateSlug(name)
        };

        if (Add(tag))
        {
            RaiseUpdatedEvent(new DatabaseUpdateEventArgs<Tag>(
                DatabaseUpdateAction.Add,
                null,
                tag
            ));

            return tag;
        }

        return null;
    }

    public bool DeleteTag(Tag tag)
    {
        if (Remove(tag))
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

    public void EditTag(Tag tag, Action<Tag> mutator)
    {
        var prev = tag with { };

        Remove(tag);
        mutator(tag);
        tag.LastEditDate = DateTime.Now;
        Add(tag);

        RaiseUpdatedEvent(new DatabaseUpdateEventArgs<Tag>(
            DatabaseUpdateAction.Edit,
            prev,
            tag
        ));
    }
}