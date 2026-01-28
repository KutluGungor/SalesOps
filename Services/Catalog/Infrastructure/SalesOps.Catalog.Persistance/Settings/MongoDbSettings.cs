namespace SalesOps.Catalog.Persistance.Settings;

public class MongoDbSettings : IMongoDbSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; } 
    public string ProductCollectionName { get; set; } 
    public string CategoryCollectionName { get; set; } 
}
