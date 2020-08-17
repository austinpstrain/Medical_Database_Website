using ClinicWeb.Model;

namespace ClinicWeb.Security
{
    public static class AccountExtensions
    {
        public static AccessLevel GetAccessLevel(this Account account)
        {
            if (account == null)
                return AccessLevel.Anonymous;

            if (account.Admin == 1)
                return AccessLevel.Admin;

            return AccessLevel.Patient;
        }
    }
}