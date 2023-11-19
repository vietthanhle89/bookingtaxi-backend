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

        public async Task DeleteDocumentation(string id)
        {
            try
            {
                await _documentationImages.DeleteOneAsync(x => x.Id.ToString() == id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public async Task<DocumentationImage?> CreateDocumentationImage(DocumentationImage obj)
        {         
            await _documentationImages.InsertOneAsync(obj);
            return obj;
        }

        public async Task<DocumentationImage?> UpdateDocumentationImage(DocumentationImage obj)
        {
            DocumentationImage newObj = obj;
            newObj.Id = obj.Id;

            await _documentationImages.ReplaceOneAsync(x=>x.Id == obj.Id, newObj);
            return obj;
        }

        public async Task<List<DocumentationImage>> GetAllDocumentationImages(string driverID)
        {
            return await _documentationImages.Find(x => x.DriverID.ToString() == driverID).ToListAsync();
        }

        public async Task<DocumentationImage> GetDocumentationImage(string id)
        {
            return await _documentationImages.Find(x => x.Id.ToString() == id).FirstOrDefaultAsync();
        }

        //Driver Car
        public async Task<DriverCar> GetDriverCar(string driverID)
        {
            return await _driverCars.Find(x => x.DriverID.ToString() == driverID).FirstOrDefaultAsync();
        }

        public async Task DeleteDriverCar(string driverID)
        {
            var obj = await GetDriverCar(driverID);
            obj.Deleted = true;
            await UpdateDriverCar(obj);
        }

        public async Task UpdateDriverCar(DriverCar obj)
        {
            await _driverCars.ReplaceOneAsync(x => x.Id.ToString() == obj.Id, obj);
        }

        public async Task<DriverCar?> CreateDriverCar(DriverCar obj)
        {
            await _driverCars.InsertOneAsync(obj);
            return obj;
        }


        //Car Type

        public async Task DeleteCarType(string id)
        {
            try
            {
                await _carTypes.DeleteOneAsync(x => x.Id.ToString() == id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public async Task<CarType?> CreateCarType(CarType obj)
        {
            await _carTypes.InsertOneAsync(obj);
            return obj;
        }

        public async Task<List<CarType>> GetAllCarTypes()
        {
            var cartypes = await _carTypes.Find(x => x.Name != "").ToListAsync();
            return cartypes;
        }

        public async Task<CarType> GetCarType(string id)
        {
            return await _carTypes.Find(x => x.Id.ToString() == id).FirstOrDefaultAsync();
        }



    }
}
