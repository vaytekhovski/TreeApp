namespace TreeApp.Api.Configuration;
public class DatabaseSettings
{
    public const string SectionName = "Database";

    public string ConnectionString { get; set; } = string.Empty;
}