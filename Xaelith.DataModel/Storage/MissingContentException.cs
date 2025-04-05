namespace Xaelith.DataModel.Storage;

public class MissingContentException : Exception
{
    public Guid EntityGuid { get; }

    public MissingContentException(Guid entityGuid, string message) : base(message)
    {
        EntityGuid = entityGuid;
    }
}