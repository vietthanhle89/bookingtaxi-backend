﻿using bookingtaxi_backend.Model;
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
        [HttpGet("GetBooking")]
        public async Task<Booking?> GetBooking(String id)
        {
            return await _bookingService.GetBooking(id);
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



        [Authorize]
        [HttpPost("BookingAssignation")]
        public async Task<IActionResult> PostBookingAssignation(BookingAssignation obj)
        {
            BookingAssignation createdObj = await _bookingService.CreateBookingAssignation(obj);
            return CreatedAtAction("PostBookingAssignation", createdObj);
        }

        [Authorize]
        [HttpPut("BookingAssignation")]
        public async Task<IActionResult> UpdateBookingAssignation(BookingAssignation obj)
        {
            var _obj = await _bookingService.GetBookingAssignation(obj.Id);

            if (_obj is null)
            {
                return NotFound();
            }

            await _bookingService.UpdateBookingAssignation(obj);

            return Ok();
        }

        [Authorize]
        [HttpDelete("BookingAssignation")]
        public async Task<IActionResult> DeleteBookingAssignation(String id)
        {
            var _obj = await _bookingService.GetBookingAssignation(id);

            if (_obj is null)
            {
                return NotFound();
            }

            await _bookingService.DeleteBookingAssignation(id);

            return Ok();
        }

        [Authorize]
        [HttpGet("GetBookingAssignation")]
        public async Task<BookingAssignation?> GeBookingAssignation(String id)
        {
            return await _bookingService.GetBookingAssignation(id);
        }

        [Authorize]
        [HttpGet("GetAllBookingAssignationsByBookingID")]
        public async Task<List<BookingAssignation>> GetAllBookingAssignationsByBookingID(string bookingID)
        {
            return await _bookingService.GetAllBookingAssignations(bookingID);
        }

        [Authorize]
        [HttpGet("GetAllBookingAssignations")]
        public async Task<List<BookingAssignation>> GetAllBookingAssignations()
        {
            return await _bookingService.GetAllBookingAssignations();
        }




        [Authorize]
        [HttpPost("BookingStatus")]
        public async Task<IActionResult> PostBookingStatus(BookingStatus obj)
        {
            BookingStatus createdObj = await _bookingService.CreateBookingStatus(obj);
            return CreatedAtAction("PostBookingStatus", createdObj);
        }

        [Authorize]
        [HttpDelete("BookingStatus")]
        public async Task<IActionResult> DeleteBookingStatus(String id)
        {
            var _obj = await _bookingService.GetBookingStatus(id);

            if (_obj is null)
            {
                return NotFound();
            }

            await _bookingService.DeleteBookingStatus(id);

            return Ok();
        }

        [Authorize]
        [HttpGet("GetBookingStatus")]
        public async Task<BookingStatus?> GeBookingStatus(String id)
        {
            return await _bookingService.GetBookingStatus(id);
        }

        [Authorize]
        [HttpGet("GetAllBookingStatuses")]
        public async Task<List<BookingStatus>> GetBookingStatuses()
        {
            return await _bookingService.GetAllBookingStatus();
        }
    }
}