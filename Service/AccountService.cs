using bookingtaxi_backend.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Diagnostics;
using static MongoDB.Bson.Serialization.Serializers.SerializerHelper;

namespace bookingtaxi_backend.Service
{
    public class AccountService
    {
        private readonly IMongoCollection<Role> _roles;
        private readonly IMongoCollection<DriverStatus> _driverStatus;
        private readonly IMongoCollection<Account> _accounts;
        private readonly IMongoCollection<Driver> _drivers;
        private readonly IMongoCollection<Customer> _customers;

        public AccountService(IOptions<DatabaseSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);

            _roles = mongoDatabase.GetCollection<Role>(settings.Value.RoleCollectionName);
            _drivers = mongoDatabase.GetCollection<Driver>(settings.Value.AccountCollectionName);
            _customers = mongoDatabase.GetCollection<Customer>(settings.Value.AccountCollectionName);
            _accounts = mongoDatabase.GetCollection<Account>(settings.Value.AccountCollectionName);
        }

        public async Task<Account?> GetAccountByCredentials(string email, string password)
        {
            return await _accounts.Find(x => x.Email == email && x.Password == password && x.Deleted != true).FirstOrDefaultAsync();
        }

        public async Task<DriverStatus?> GetDriverStatus(string id) => await _driverStatus.Find(x => x.Id.ToString() == id && x.Deleted != true).FirstOrDefaultAsync();

        //Account
        public async Task<bool> IsEmailExisted(string email)
        {
            var exist = await _accounts.Find(x => x.Email.ToLower() == email).FirstOrDefaultAsync();
            
            if (exist == null)
            {
                return false;
            }

            return true;
        }
        private async Task UpdateAccount(Account updatedObj)
        {
            try
            {
                Account newObj = updatedObj;
                newObj.Id = updatedObj.Id;
                await _accounts.ReplaceOneAsync(x => x.Id.ToString() == updatedObj.Id, newObj);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

        }
        public async Task<Account?> GetAccount(string id) => await _accounts.Find(x => x.Id.ToString() == id && x.Deleted != true).FirstOrDefaultAsync();
        public async Task RemoveAccount(string id)
        {
            var obj = await GetAccount(id);
            obj.Deleted = true;
            await UpdateAccount(obj);
        }


        //Administrator
        public async Task<Administrator?> CreateAdministrator(Account obj) {
            obj.RoleID = ROLEID.ADMIN;
            obj.CreatedDate = DateTime.Now;
            obj.Deleted = false;
            await _accounts.InsertOneAsync(obj);
            return (Administrator?)obj;
        }
        public async Task UpdateAdministrator(Administrator obj) {
            try
            {
                await _accounts.ReplaceOneAsync(x => x.Id.ToString() == obj.Id, obj);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        public async Task<List<Account>> GetAllAdministrators() => await _accounts.Find(x => x.Deleted != true && x.RoleID.ToString() == ROLEID.ADMIN).ToListAsync();
        public async Task<Account?> GetAdministrator(string id) => await _accounts.Find(x => x.Id.ToString() == id && x.Deleted != true && x.RoleID.ToString() == ROLEID.ADMIN).FirstOrDefaultAsync();
        public async Task<Account?> GetAdministrator(string username, string password) => await _accounts.Find(x => x.Email == username && x.Password == password && x.RoleID.ToString() == ROLEID.ADMIN && x.Deleted != true).FirstOrDefaultAsync();


        //Supporter
        public async Task<List<Account>> GetAllSupporters() => await _accounts.Find(x => x.Deleted != true && x.RoleID.ToString() == ROLEID.SUPPORTER).ToListAsync();
        public async Task<Account?> GetSupporter(string id) => await _accounts.Find(x => x.Id.ToString() == id && x.Deleted != true && x.RoleID.ToString() == ROLEID.SUPPORTER).FirstOrDefaultAsync();
        public async Task<Account?> GetSupporter(string username, string password) => await _accounts.Find(x => x.Email == username && x.Password == password && x.RoleID.ToString() == ROLEID.SUPPORTER && x.Deleted != true).FirstOrDefaultAsync();
        public async Task<Supporter?> CreateSupporter(Supporter obj)
        {
            obj.RoleID = ROLEID.SUPPORTER;
            obj.CreatedDate = DateTime.Now;
            obj.Deleted = false;
            await _accounts.InsertOneAsync(obj);
            return obj;
        }
        public async Task UpdateSupporter(Supporter obj)
        {
            try
            {
                Supporter newObj = obj;
                newObj.Id = obj.Id;

                await _accounts.ReplaceOneAsync(x => x.Id.ToString() == obj.Id, newObj);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }


        //Driver
        public async Task<List<Driver>> GetAllDrivers()
        {
            return await _drivers.Find(x => x.Deleted != true && x.RoleID.ToString() == ROLEID.DRIVER).ToListAsync();
        }
        public async Task<Driver?> GetDriver(string id) => await _drivers.Find(x => x.Id.ToString() == id && x.Deleted != true && x.RoleID.ToString() == ROLEID.DRIVER).FirstOrDefaultAsync();
        public async Task<Driver?> GetDriver(string username, string password) => await _drivers.Find(x => x.Email == username && x.Password == password && x.RoleID.ToString() == ROLEID.DRIVER && x.Deleted != true).FirstOrDefaultAsync();

        public async Task<Driver?> CreateDriver(Driver obj)
        {
            obj.RoleID = ROLEID.DRIVER;
            obj.CreatedDate = DateTime.Now;
            obj.Deleted = false;
            obj.Approved = false;
            await _accounts.InsertOneAsync(obj);
            return obj;            
        }
        public async Task UpdateDriver(Driver obj)
        {
            try
            {
                Driver newObj = obj;
                newObj.Id = obj.Id;
                await _drivers.ReplaceOneAsync(x => x.Id.ToString() == obj.Id, newObj);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        public async Task ApproveDriver(string accountID, bool approval) {
            var driver = await _drivers.Find(x => x.Id == accountID && x.Deleted != true).FirstOrDefaultAsync();
            driver.Approved = approval;

            await UpdateDriver(driver);
        }
        public async Task ChangeDriverStatus(string accountID, string driverStatusID)
        {
            var driver = await _drivers.Find(x => x.Id == accountID && x.Deleted != true).FirstOrDefaultAsync();
            var status = await GetDriverStatus(driverStatusID);
            if (status != null) {
                driver.DriverStatusID = driverStatusID;

                await UpdateDriver(driver);
            }
        }
        
        //Customer
        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _customers.Find(x => x.Deleted != true && x.RoleID.ToString() == ROLEID.CUSTOMER).ToListAsync();
        }
        public async Task<Customer?> GetCustomer(string id) => await _customers.Find(x => x.Id.ToString() == id && x.Deleted != true && x.RoleID.ToString() == ROLEID.CUSTOMER).FirstOrDefaultAsync();
        public async Task<Customer?> FindCustomerByPhone(string phone) => await _customers.Find(x => x.Phone.ToString() == phone && x.Deleted != true && x.RoleID.ToString() == ROLEID.CUSTOMER).FirstOrDefaultAsync();


        
        public async Task<Customer?> GetCustomer(string username, string password) => await _customers.Find(x => x.Email == username && x.Password == password && x.RoleID.ToString() == ROLEID.CUSTOMER && x.Deleted != true).FirstOrDefaultAsync();
        public async Task<Customer?> CreateCustomer(Customer obj)
        {
            obj.RoleID = ROLEID.CUSTOMER;
            obj.CreatedDate = DateTime.Now;
            obj.Deleted = false;
            await _accounts.InsertOneAsync(obj);
            return obj;
        }
        public async Task UpdateCustomer(Customer obj)
        {
            try
            {
                await _customers.ReplaceOneAsync(x => x.Id.ToString() == obj.Id, obj);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        

        //Role
        public async Task DeleteRole(string id)
        {
            try
            {
                await _roles.DeleteOneAsync(x => x.Id.ToString() == id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        public async Task<Role?> CreateCarType(Role obj)
        {
            await _roles.InsertOneAsync(obj);
            return obj;
        }
        public async Task<List<Role>> GetAllRoles()
        {
            return await _roles.Find(x => x.Id != null).ToListAsync();
        }
        public async Task<Role> GetRole(string id)
        {
            return await _roles.Find(x => x.Id.ToString() == id).FirstOrDefaultAsync();
        }
    }
}
