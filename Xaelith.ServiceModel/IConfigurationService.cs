namespace Xaelith.ServiceModel;

using Xaelith.DataModel.Configuration;

public interface IConfigurationService
{
    string SettingsFilePath { get; }
    Settings Settings { get; }
    
    void Reload();
    void Save();
}