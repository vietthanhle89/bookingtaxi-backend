using bookingtaxi_backend.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Diagnostics;
using static MongoDB.Bson.Serialization.Serializers.SerializerHelper;

namespace bookingtaxi_backend.Service
{
    public class DriverPropertiesService
    {
        private readonly IMongoCollection<Driver> _drivers;
        private readonly IMongoCollection<DocumentationImage> _documentationImages;
        private readonly IMongoCollection<DriverCar> _driverCars;
        private readonly IMongoCollection<CarType> _carTypes;

        public DriverPropertiesService(IOptions<DatabaseSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);
            
            _drivers = mongoDatabase.GetCollection<Driver>(settings.Value.AccountCollectionName);

            _documentationImages = mongoDatabase.GetCollection<DocumentationImage>(settings.Value.DocumentationImageCollectionName);
            _driverCars = mongoDatabase.GetCollection<DriverCar>(settings.Value.DriverCarCollectionName);
            _carTypes = mongoDatabase.GetCollection<CarType>(settings.Value.CarTypeCollectionName);
        }









        
    }
}
