namespace Xaelith.ServiceModel;

public interface ILanguageService
{
    string? CurrentLanguage { get; }
    
    void LoadTranslationFile(string language);
    string Translate(string key);
}