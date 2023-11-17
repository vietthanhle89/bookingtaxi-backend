using bookingtaxi_backend.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Diagnostics;
using static MongoDB.Bson.Serialization.Serializers.SerializerHelper;

namespace bookingtaxi_backend.Service
{
    public class BookingService
    {
        private readonly IMongoCollection<Booking> _booking;
        private readonly IMongoCollection<BookingStatus> _bookingStatus;
        private readonly IMongoCollection<BookingAssignation> _bookingAssignations;
        private readonly IMongoCollection<TripRecord> _tripRecords;


        public BookingService(IOptions<DatabaseSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);

            _booking = mongoDatabase.GetCollection<Booking>(settings.Value.BookingCollectionName);
            _bookingStatus = mongoDatabase.GetCollection<BookingStatus>(settings.Value.BookingStatusCollectionName);
            _bookingAssignations = mongoDatabase.GetCollection<BookingAssignation>(settings.Value.BookingAssignationCollectionName);
            _tripRecords = mongoDatabase.GetCollection<TripRecord>(settings.Value.BookingAssignationCollectionName);
        }

        //Booking
        public async Task<List<Booking>> GetAllBookings()
        {
            return await _booking.Find(x => x.Deleted != true).ToListAsync();
        }

        public async Task<List<Booking>> GetAllWaitingBookings()
        {
            return await _booking.Find(x => x.Deleted != true && x.BookingStatusID == BookingStatusEnum.WAITING).ToListAsync();
        }

        public async Task<List<Booking>> GetAllBookingsByCustomer(string customerID)
        {
            return await _booking.Find(x => x.Deleted != true && x.CustomerID == customerID ).ToListAsync();
        }
        public async Task<Booking?> GetBooking(string id) => await _booking.Find(x => x.Id.ToString() == id && x.Deleted != true).FirstOrDefaultAsync();
        public async Task<Booking?> CreateBooking(Booking obj)
        {
            obj.BookingDate = DateTime.Now;
            obj.Deleted = false;
            await _booking.InsertOneAsync(obj);
            return obj;
        }
        public async Task UpdateBooking(Booking obj)
        {
            try
            {
                obj.Id = obj.Id.ToString();
                await _booking.UpdateOneAsync(x => x.Id.ToString() == obj.Id,
                    Builders<Booking>.Update.Set(rec => rec.BookingStatusID, obj.BookingStatusID));
                //await _booking.ReplaceOneAsync(x => x.Id.ToString() == obj.Id, obj);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        public async Task DeleteBooking(string id)
        {
            var obj = await _booking.Find(x => x.Id.ToString() == id && x.Deleted != true).FirstOrDefaultAsync();

            if (obj != null)
            {
                obj.Deleted = true;
                await _booking.ReplaceOneAsync(x => x.Id.ToString() == obj.Id, obj);
            }
        }


        //BookingAssignation
        public async Task<List<BookingAssignation>> GetAllBookingAssignations()
        {
            return await _bookingAssignations.Find(x => x.Deleted != true).ToListAsync();
        }
        public async Task<List<BookingAssignation>> GetAllBookingAssignations(string bookingID)
        {
            return await _bookingAssignations.Find(x => x.Deleted != true && x.BookingID == bookingID).ToListAsync();
        }

        public async Task<bool> CheckBookingAssignation(string driverID, string bookingID)
        {
            var _obj = await _bookingAssignations.Find(x => x.BookingID == bookingID && (x.DriverID == driverID || x.Deleted == false)).FirstOrDefaultAsync();

            if (_obj == null)
            {
                return true;
            }

            return false;
        }

        public async Task<BookingAssignation?> GetBookingAssignation(string id) => await _bookingAssignations.Find(x => x.Id.ToString() == id && x.Deleted != true).FirstOrDefaultAsync();
        public async Task<BookingAssignation?> CreateBookingAssignation(BookingAssignation obj)
        {            
            obj.AssignDate = DateTime.Now;
            obj.Deleted = false;
            await _bookingAssignations.InsertOneAsync(obj);

            return obj;            
        }
        public async Task UpdateBookingAssignation(BookingAssignation obj)
        {
            try
            {
                await _bookingAssignations.ReplaceOneAsync(x => x.Id.ToString() == obj.Id, obj);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        public async Task DeleteBookingAssignation(string id)
        {
            var obj = await _bookingAssignations.Find(x => x.Id.ToString() == id && x.Deleted != true).FirstOrDefaultAsync();

            if (obj != null) {
                obj.Deleted = true;
                await _bookingAssignations.ReplaceOneAsync(x => x.Id.ToString() == obj.Id, obj);
            }
        }


        //BookingStatus
        public async Task DeleteBookingStatus(string id)
        {
            try
            {
                await _bookingStatus.DeleteOneAsync(x => x.Id.ToString() == id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        public async Task<BookingStatus?> CreateBookingStatus(BookingStatus obj)
        {
            await _bookingStatus.InsertOneAsync(obj);
            return obj;
        }
        public async Task<List<BookingStatus>> GetAllBookingStatus()
        {
            return await _bookingStatus.Find(x => x.Id != null).ToListAsync();
        }
        public async Task<BookingStatus> GetBookingStatus(string id)
        {
            return await _bookingStatus.Find(x => x.Id.ToString() == id).FirstOrDefaultAsync();
        }

        //TripRecord
        public async Task DeleteTripRecord(string id)
        {
            try
            {
                await _tripRecords.DeleteOneAsync(x => x.Id.ToString() == id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        public async Task<TripRecord?> CreateTripRecord(TripRecord obj)
        {
            await _tripRecords.InsertOneAsync(obj);
            return obj;
        }
        public async Task<List<TripRecord>> GetAlTripRecords(string bookingID)
        {
            return await _tripRecords.Find(x => x.BookingID == bookingID).ToListAsync();
        }

        public async Task<TripRecord> GetTripRecord(string id)
        {
            return await _tripRecords.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

    }
}
