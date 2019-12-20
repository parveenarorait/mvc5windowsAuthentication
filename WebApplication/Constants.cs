namespace WebApplication
{
    public class CustomClaimTypes
    {
        public const string Permission = "Permission";
        public const string DisplayName = "DisplayName";
        public const string FirstName = "FirstName";
        public const string Projects = "Projects";
        public const string TimeZone = "TimeZone";
        public const string UserId = "UserId";
    }

    public enum Permissions : int
    {
        NotSet = 0, //error condition
        HomePageAccess = 1,
        AccessAll = 2,
        AboutPageAccess = 3,
        AdminPrivileges = 4,
    }
}