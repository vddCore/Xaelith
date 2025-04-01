namespace Xaelith.Common.Utilities;

using Slugify;

public static class UrlUtilities
{
    private static SlugHelper _slugHelper = new(new SlugHelperConfiguration
    {
        MaximumLength = 256
    });

    public static string GenerateSlug(string text)
        => _slugHelper.GenerateSlug(text);
}