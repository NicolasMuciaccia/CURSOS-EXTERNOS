namespace APICatalogo.Logging
{
    public class CustomLoggerProviderConfiguration
    {
        // Classe mais abstrata, define o id é o loglevel padrão
        public LogLevel LogLevel { get; set; } = LogLevel.Warning;
        public int EventId { get; set; } = 0;
    }
}
