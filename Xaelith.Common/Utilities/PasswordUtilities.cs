namespace Xaelith.Common.Utilities;

using System.Security.Cryptography;
using System.Text;

public static class PasswordUtilities
{
    private const string PasswordCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()-_=+[]{}|;:',.<>?/`~";
    
    public static string GenerateSha512Hash(string password, string salt)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentOutOfRangeException(nameof(password), "Password cannot be empty.");
        
        if (string.IsNullOrWhiteSpace(salt))
            throw new ArgumentOutOfRangeException(nameof(salt), "Salt cannot be empty.");

        var combinedStringUtf8 = Encoding.UTF8.GetBytes($"{password}.{salt}");
        var sha512 = SHA512.HashData(combinedStringUtf8);
        var sb = new StringBuilder(sha512.Length / 2);
 
        foreach (var b in sha512)
        {
            sb.Append(b.ToString("X2"));
        }
        
        return sb.ToString();
    }

    public static string GenerateRandomString(int length)
        => RandomNumberGenerator.GetString(PasswordCharacters, length);
}