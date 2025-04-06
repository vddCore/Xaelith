namespace Xaelith.Services.Localization;

using System.Text.Json;
using Xaelith.Common;
using Xaelith.DataModel;
using Xaelith.DataModel.Configuration;
using Xaelith.ServiceModel;

public class LanguageService : ILanguageService
{
    private readonly Settings _settings;
    private Translation? _currentTranslation;

    public string? CurrentLanguage
    {
        get => _currentTranslation?.Language;
        set => LoadTranslationFile(value ?? "en_US");
    }

    public LanguageService(IConfigurationService configurationService)
    {
        _settings = configurationService.Settings;
        CurrentLanguage = _settings.General.Language;
    }

    public void LoadTranslationFile(string language)
    {
        var langFileDir = Path.Combine(KnownPaths.Core, _settings.Core.LanguageDirectoryName);
        
        var translationFile = Directory.GetFiles(
            langFileDir, 
            $"{language}.json"
        ).FirstOrDefault();

        if (translationFile == null)
            throw new FileNotFoundException($"The file '{language}.json' was not found in '{langFileDir}'.");

        using (var sr = new StreamReader(translationFile))
        {
            _currentTranslation = JsonSerializer.Deserialize<Translation>(
                sr.ReadToEnd()
            ) ?? throw new InvalidOperationException($"Failed to deserialize the translation file '{language}.json'.");
        }
    }

    public string Translate(string key)
    {
        if (_currentTranslation == null)
            return key;

        return _currentTranslation[key];
    }
}