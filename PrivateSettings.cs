namespace ChatBot
{
    public static class PrivateSettings
    {
        public static readonly string TOKEN = "";
        
        // DataBase settings
        private const string Host = "ec2-54-74-77-126.eu-west-1.compute.amazonaws.com";
        private const int Port = 5432;
        private const string Username = "hikrpbttlttcwq";
        private const string Password = "b29d4a14c532b6a3a51930e0cfcd6df1160314534c54663a34ecb15a9a5c96b7";
        private const string Datebase = "d5jn967h0si61h";
        private const bool Pooling = true;
        private const string SSLMode = "Require";
        private const bool TrueServerCertificate = true;
        
        /// <summary>
        /// string for connecting to DB
        /// </summary>
        public static readonly string ConnectionString = $"host={Host};" +
                                                         $"username={Username};" +
                                                         $"password={Password};" +
                                                         $"database={Datebase};" +
                                                         $"port={Port};" +
                                                         $"Sslmode=Require;" +
                                                         $"Trust Server Certificate=true";
    }
}