namespace TaskLibrary.Web
{
    public static class Settings
    {
        //public static string ApiRoot = "http://localhost:20000/api";

        public static string ApiRoot = "http://host.docker.internal:20000/api";
        //public static string IdentityRoot = "http://localhost:20001";
        public static string IdentityRoot = "http://host.docker.internal:20001";
        
        public static string ClientId = "frontend";
        public static string ClientSecret = "secret";
    }
}
