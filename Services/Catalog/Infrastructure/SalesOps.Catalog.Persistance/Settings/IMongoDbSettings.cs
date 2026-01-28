namespace SalesOps.Catalog.Persistance.Settings;

public interface IMongoDbSettings
{
    string ConnectionString { get; }
    string DatabaseName { get; }
    string ProductCollectionName { get; }
    string CategoryCollectionName { get; }
}
