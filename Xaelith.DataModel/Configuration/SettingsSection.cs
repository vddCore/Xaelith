namespace Xaelith.DataModel.Configuration;

using System.ComponentModel;
using System.Reflection;
using System.Text.Json.Serialization;

public abstract class SettingsSection : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public object? this[string jsonPropertyKey] 
        => GetValue<object?>(jsonPropertyKey);

    public string GetJsonPropertyKey(string managedPropertyName)
    {
        var properties = GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.Name == managedPropertyName);
        
        foreach (var p in properties)
        {
            var nameAttribute = p.GetCustomAttribute<JsonPropertyNameAttribute>();

            if (nameAttribute != null)
                return nameAttribute.Name;
        }

        return managedPropertyName;
    }
    
    public T? GetValue<T>(string jsonPropertyKey)
    {
        var properties = GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.PropertyType == typeof(T));
        
        foreach (var p in properties)
        {
            var nameAttribute = p.GetCustomAttribute<JsonPropertyNameAttribute>();

            if (nameAttribute == null)
                return default(T);

            if (nameAttribute.Name == jsonPropertyKey)
                return (T?)p.GetValue(this);
        }
        
        return default(T);
    }
}