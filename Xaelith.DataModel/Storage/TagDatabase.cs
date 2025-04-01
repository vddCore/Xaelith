namespace Xaelith.DataModel.Storage;

using Xaelith.DataModel.Abstract;

public class TagDatabase : FlatFileDatabase<Tag>
{
    public Tag CreateNewTag(User author, string name)
    {
        var tag = new Tag
        {
            CreatedDate = DateTime.Now,
            Author = author,
            Name = name,
            
        };
    }
}