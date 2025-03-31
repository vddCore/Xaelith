namespace Xaelith;

public static class KnownPaths
{
    public static readonly string Data = Path.Combine(AppContext.BaseDirectory, "xa-data");
    public static readonly string Configuration = Path.Combine(Data, "config");
    public static readonly string Content = Path.Combine(Data, "content");
    public static readonly string Databases = Path.Combine(Data, "databases");
    
    public static readonly string Plugins = Path.Combine(AppContext.BaseDirectory, "xa-plugins");

    static KnownPaths()
    {
        Directory.CreateDirectory(Data);
        Directory.CreateDirectory(Configuration);
        Directory.CreateDirectory(Content);
        Directory.CreateDirectory(Databases);
        Directory.CreateDirectory(Plugins);
    }
}