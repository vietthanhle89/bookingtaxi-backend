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
        private readonly IMongoCollection<DriverCar> _driverCar;

        public BookingService(IOptions<DatabaseSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);

            _booking = mongoDatabase.GetCollection<Booking>(settings.Value.BookingCollectionName);
            _bookingStatus = mongoDatabase.GetCollection<BookingStatus>(settings.Value.BookingStatusCollectionName);
            _bookingAssignations = mongoDatabase.GetCollection<BookingAssignation>(settings.Value.BookingAssignationCollectionName);
            _tripRecords = mongoDatabase.GetCollection<TripRecord>(settings.Value.TripRecordCollectionName);
            _driverCar = mongoDatabase.GetCollection<DriverCar>(settings.Value.DriverCarCollectionName);
        }

        //Booking
        public async Task<List<Booking>> GetAllBookings()
        {
            return await _booking.Find(x => x.Deleted != true).ToListAsync();
        }
        
        public async Task<List<Booking>> GetInProgressBookings(string driverID)
        {
            List<Booking> result = new List<Booking>();
            var assignations = await _bookingAssignations.Find(x => x.Deleted == false && x.DriverID == driverID).ToListAsync();            

            foreach (var assignation in assignations) { 
                var booking = await _booking.Find(x => x.Deleted != true && assignation.BookingID == x.Id.ToString() && x.BookingStatusID != BookingStatusEnum.COMPLETED && x.BookingStatusID != BookingStatusEnum.CANCELLED).FirstOrDefaultAsync();
                if (booking != null) { result.Add(booking); }
            }

            return result;
        }

        public async Task<List<Booking>> GetCompletedBookings(string driverID)
        {
            List<Booking> result = new List<Booking>();
            var assignations = await _bookingAssignations.Find(x => x.Deleted == false && x.DriverID == driverID).ToListAsync();

            foreach (var assignation in assignations)
            {
                var booking = await _booking.Find(x => x.Deleted != true && assignation.BookingID == x.Id.ToString() && x.BookingStatusID == BookingStatusEnum.COMPLETED).FirstOrDefaultAsync();
                if (booking != null) { result.Add(booking); }
            }

            return result;
        }
        


        public async Task<List<Booking>> GetAllWaitingBookings()
        {
            return await _booking.Find(x => x.Deleted != true && x.BookingStatusID == BookingStatusEnum.WAITING).ToListAsync();
        }

        public async Task<List<Booking>> GetAllWaitingBookings(string driverID)
        {
            var driverCar = await _driverCar.Find(x => x.Deleted == false && x.DriverID == driverID).FirstOrDefaultAsync();
            List<Booking> bookings;
            List<Booking> finalBookings = new List<Booking>();

            if (driverCar.CarTypeID == CarTypeEnum.FOUR_SEAT)
            {
                 bookings = await _booking.Find(x => x.Deleted != true && x.BookingStatusID == BookingStatusEnum.WAITING && driverCar.CarTypeID == x.CarTypeID).ToListAsync();
            }
            else  {
                bookings = await _booking.Find(x => x.Deleted != true && x.BookingStatusID == BookingStatusEnum.WAITING).ToListAsync();
            }

            for (int i = 0; i < bookings.Count; i++)
            {
                var tmp = await _bookingAssignations.Find(x => x.DriverID == driverID && x.BookingID == bookings[i].Id).FirstOrDefaultAsync();

                if (tmp == null) {
                    finalBookings.Add(bookings[i]);
                }
            }

            return finalBookings;

        }

        public async Task<Booking> GetMyInProgressBooking(string customerID)
        {
            return await _booking.Find(x => x.Deleted != true && x.CustomerID == customerID && x.BookingStatusID != BookingStatusEnum.COMPLETED && x.BookingStatusID != BookingStatusEnum.CANCELLED).FirstOrDefaultAsync();
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
                var newObj = obj;
                newObj.Id = obj.Id;
                newObj.Note = "Hello";
                await _booking.ReplaceOneAsync(x => x.Id == obj.Id, newObj);
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
        public async Task<bool> IsDriverAvailableForBooking(string driverID)
        {
            var assignations = await _bookingAssignations.Find(x => x.DriverID == driverID && x.Deleted == false).ToListAsync();
            foreach (var item in assignations)
            {
                var tmp = await _booking.Find(b => b.Deleted == false && b.Id == item.Id && b.BookingStatusID != BookingStatusEnum.COMPLETED && b.BookingStatusID != BookingStatusEnum.CANCELLED).FirstOrDefaultAsync();
                if (tmp != null) {
                    return false;
                }
            }

            return true;
        }


        
        public async Task<BookingAssignation?> GetBookingAssignation(string id) => await _bookingAssignations.Find(x => x.Id.ToString() == id && x.Deleted != true).FirstOrDefaultAsync();
        public async Task<BookingAssignation?> GetBookingAssignation(string bookingID, string driverID) => await _bookingAssignations.Find(x => x.BookingID == bookingID && (x.DriverID == driverID || x.Deleted == false)).FirstOrDefaultAsync();
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

        public async Task<bool> DriverCancelBooking(string driverID, string bookingID)
        {
            try
            {
                var booking = await GetBooking(bookingID);
                booking.BookingStatusID = BookingStatusEnum.WAITING;
                await UpdateBooking(booking);
                var asign = await GetBookingAssignation(bookingID, driverID);

                await DeleteBookingAssignation(asign.Id);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return false;
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
