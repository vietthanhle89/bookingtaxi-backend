using bookingtaxi_backend.Model;
using bookingtaxi_backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bookingtaxi_backend.Controller
{
    public class BookingController : ControllerBase
    {
        private readonly BookingService _bookingService;


        public BookingController(BookingService bookingService, EmailService emailService)
        {
            _bookingService = bookingService;
        }

        [Authorize]
        [HttpPost("Booking")]
        public async Task<IActionResult> PostBooking(Booking obj)
        {
            Booking createdObj = await _bookingService.CreateBooking(obj);
            return CreatedAtAction("PostBooking", createdObj);
        }

        [Authorize]
        [HttpPut("Booking")]
        public async Task<IActionResult> UpdateBooking(Booking obj)
        {
            var _obj = await _bookingService.GetBooking(obj.Id);

            if (_obj is null)
            {
                return NotFound();
            }

            await _bookingService.UpdateBooking(obj);

            return Ok();
        }

        [Authorize]
        [HttpDelete("Booking")]
        public async Task<IActionResult> DeleteBooking(String id)
        {
            var _obj = await _bookingService.GetBooking(id);

            if (_obj is null)
            {
                return NotFound();
            }

            await _bookingService.DeleteBooking(id);

            return Ok();
        }

        [Authorize]
        [HttpGet("GetAllBookings")]
        public async Task<List<Booking>> GetAllBookings()
        {
            return await _bookingService.GetAllBookings();
        }

        [Authorize]
        [HttpGet("GetAllBookingsByCustomer")]
        public async Task<List<Booking>> GetAllBookingsByCustomer(string customerID)
        {
            return await _bookingService.GetAllBookingsByCustomer(customerID);
        }

    }
}
