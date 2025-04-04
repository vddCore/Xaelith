namespace Xaelith.DataModel.Abstract;

public interface IAuthored
{
    Guid AuthorUserId { get; set; }
}