using bookingtaxi_backend.Model;
using bookingtaxi_backend.Service;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Driver.Core.Connections;
using System;
using System.Globalization;

namespace bookingtaxi_backend.Hub
{
    public class TripMonitorHub : Hub<ITypeHub>
    {
        private readonly BookingService _bookingService;

        public TripMonitorHub(BookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task Report(string bookingID, string lng, string lat)
        {
            var a = new TripRecord()
            {
                Id = "",
                BookingID = bookingID,
                Date = DateTime.Now,
                Lat = lat,
                Long = lng
            };

            await _bookingService.CreateTripRecord(a);
            await Clients.Group("trip-"+bookingID).ReceivedMessage(lng, lat);
        }

        public async Task AddPersonToGroup(string ConnectionId, string bookingID)
        {
            await Groups.AddToGroupAsync(ConnectionId, "trip-" + bookingID);
        }

        public async Task RemovePersonFromGroup(string ConnectionId, string bookingID)
        {
            await Groups.RemoveFromGroupAsync(ConnectionId, "trip-" + bookingID);
        }

        public async Task AddMeToGroup(string bookingID)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "trip-" + bookingID);
        }

        public async Task RemoveMeFromGroup(string bookingID)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "trip-" + bookingID);
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

    }

}

