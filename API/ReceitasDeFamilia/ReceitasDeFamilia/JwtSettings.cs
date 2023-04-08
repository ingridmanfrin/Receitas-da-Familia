namespace ReceitasDeFamilia
{
    public static class JwtSettings
    {
        public static string Issuer { get; set; }
        public static string Audience { get; set; }
        public static string Key { get; set; }
       
       public static void PopulateSettings(string issuer, string audience, string key) { 
            Issuer = issuer;
            Audience = audience;
            Key = key;
        }
    }
}
