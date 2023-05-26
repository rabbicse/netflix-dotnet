namespace Work.Rabbi.Common.Infrastructure.Configs
{
    public class DatabaseSettings
    {
        public ConnectionSettings? ConnectionStrings { get; set; }
        public int MaxRetryCount { get; set; }
        public int CommandTimeOut { get; set; }
        public bool EnableDetailedError { get; set; }
        public bool EnableSensitiveDataLogging { get; set; }

    }
}