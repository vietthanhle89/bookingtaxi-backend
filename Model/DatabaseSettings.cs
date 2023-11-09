namespace bookingtaxi_backend.Model
{
    public class DatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string RoleCollectionName { get; set; } = null!;
        public string AccountCollectionName { get; set; } = null!;
        
    }
}
