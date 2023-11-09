using bookingtaxi_backend.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using static MongoDB.Bson.Serialization.Serializers.SerializerHelper;

namespace bookingtaxi_backend.Service
{
    public class AccountService
    {
        private readonly IMongoCollection<Role> _roles;
        private readonly IMongoCollection<Account> _accounts;


        public AccountService(IOptions<DatabaseSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);

            _roles = mongoDatabase.GetCollection<Role>(settings.Value.RoleCollectionName);
            _accounts = mongoDatabase.GetCollection<Account>(settings.Value.AccountCollectionName);
            
        }

        public async Task<Account?> GetAccountByCredentials(string email, string password)
        {
            return await _accounts.Find(x => x.Email == email && x.Password == password && x.Deleted != true).FirstOrDefaultAsync();
        }

        public async Task<Role?> GetRole(string id) => await _roles.Find(x => x.Id.ToString() == id && x.Deleted != true).FirstOrDefaultAsync();

    }
}
