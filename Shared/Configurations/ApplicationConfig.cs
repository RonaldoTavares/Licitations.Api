namespace Shared.Configurations
{
    public class ApplicationConfig
    {
        public ApplicationConfig()
        {
            Database = new DatabaseConfig();
        }

        public DatabaseConfig Database { get; set; }
    }

    public class DatabaseConfig
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string AssemblyName { get; set; }
        public string DbFactoryName { get; set; }
    }
}
