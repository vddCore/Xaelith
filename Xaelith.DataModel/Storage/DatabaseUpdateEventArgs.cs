namespace Xaelith.DataModel.Storage;

public sealed class DatabaseUpdateEventArgs<T> where T : class, new()
{
    public DatabaseUpdateAction Action { get; set; }
    
    public T? OldValue { get; set; }
    public T? NewValue { get; set; }

    internal DatabaseUpdateEventArgs(DatabaseUpdateAction action, T? oldValue, T? newValue)
    {
        Action = action;
        OldValue = oldValue;
        NewValue = newValue;
    }
}