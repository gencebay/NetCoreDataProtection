using Microsoft.AspNetCore.DataProtection;

namespace NetCore.Hosting
{
    public class DefaultDataProtectorProvider
    {
        private readonly IDataProtector _protector;
        public DefaultDataProtectorProvider(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector("App.Key.V1");
        }

        public string Protect(string input)
        {
            // protect the payload
            string protectedPayload = _protector.Protect(input);
            return protectedPayload;
        }

        public string Unprotect(string protectedPayload)
        {
            // unprotect the payload
            string unprotectedPayload = _protector.Unprotect(protectedPayload);
            return unprotectedPayload;
        }
    }
}