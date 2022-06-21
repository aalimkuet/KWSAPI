namespace KWS.Infrastructure
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string FileUploadPath { get; set; }
        public string DbServer { get; set; }
        public string ConnectionString { get; set; }
        public double AccessTokenExpirationMinutes { get; set; }
        public string RefreshTokenSecret { get; set; }
        public double RefreshTokenExpirationMinutes { get; set; }
    }
}
