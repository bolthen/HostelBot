using System.Security.Claims;

namespace WebUi;

public static class ClaimPrincipalExtenstion
{
    public static ValueParser GetClaimValue(this ClaimsPrincipal user, string key)
    {
        var claim = user.Claims.FirstOrDefault(x => x.Type == key);
        if (claim is null)
            return new ValueParser(null);
        return new ValueParser(claim.Value);
    }

    public class ValueParser
    {
        private readonly string? value;

        public ValueParser(string? value) => this.value = value;

        public bool TryParseInt(out int result)
        {
            return int.TryParse(value, out result);
        }

        public bool TryGetString(out string result)
        {
            result = "";
            if (value is null)
                return false;
            result = value;
            return true;
        }
    }
}