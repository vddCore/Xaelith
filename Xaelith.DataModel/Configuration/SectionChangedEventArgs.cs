namespace Xaelith.DataModel.Configuration;

public class SectionChangedEventArgs
{
    public SettingsSection Section { get; }
    public string ManagedPropertyName { get; }
    public string JsonPropertyKey { get; }
    public object? Value { get; }

    internal SectionChangedEventArgs(
        SettingsSection section,
        string managedPropertyName,
        string jsonPropertyKey,
        object? value)
    {
        Section = section;
        ManagedPropertyName = managedPropertyName;
        JsonPropertyKey = jsonPropertyKey;
        Value = value;
    }
}