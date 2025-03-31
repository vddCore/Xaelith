namespace Xaelith.DataModel.Abstract;

public interface IDated
{
    DateTime CreatedDate { get; set; }
    DateTime? LastEditDate { get; set; }
}