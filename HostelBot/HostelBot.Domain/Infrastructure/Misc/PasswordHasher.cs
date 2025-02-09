using System.Security.Cryptography;

namespace HostelBot.Domain.Infrastructure;

public static class PasswordHasher
{
    private const int SaltSize = 16;
    private const int HashSize = 20;
    private const string HashId = "$MYHASH$V1$";
        
    public static string Hash(string password, int iterations = 10000)
    {
        byte[] salt;
        new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);

        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
        var hash = pbkdf2.GetBytes(HashSize);

        var hashBytes = new byte[SaltSize + HashSize];
        Array.Copy(salt, 0, hashBytes, 0, SaltSize);
        Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

        var base64Hash = Convert.ToBase64String(hashBytes);
        return $"{HashId}{iterations}${base64Hash}";
    }

    private static bool IsHashSupported(string hashString) => hashString.Contains(HashId);

    public static bool Verify(string checkPassword, string hashedPassword)
    {
        if (!IsHashSupported(hashedPassword))
            throw new NotSupportedException("This hashtype is not supported");

        var splittedHashString = hashedPassword.Replace(HashId, "").Split('$');
        var iterations = int.Parse(splittedHashString[0]);
        var base64Hash = splittedHashString[1];

        var hashBytes = Convert.FromBase64String(base64Hash);

        var salt = new byte[SaltSize];
        Array.Copy(hashBytes, 0, salt, 0, SaltSize);

        var pbkdf2 = new Rfc2898DeriveBytes(checkPassword, salt, iterations);
        var hash = pbkdf2.GetBytes(HashSize);

        for (var i = 0; i < HashSize; i++)
            if (hashBytes[i + SaltSize] != hash[i])
                return false;

        return true;
    }
}