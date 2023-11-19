using bookingtaxi_backend.Service;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Bson;

namespace bookingtaxi_backend.Hub
{

    public class BookingHub : Hub<ITypeHub>
    {
        private readonly BookingService _bookingService;

        public BookingHub(BookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task GetAllBookings()
        {
            while (true)
            {
                var a = await _bookingService.GetAllBookings();

                await Clients.Caller.ReceivedMessage(a);
                await Task.Delay(new TimeSpan(0, 0, 5));
            }
        }

        public async Task GetWaitingBookings(string driverID)
        {
            while (true)
            {
                var a = await _bookingService.GetAllWaitingBookings(driverID);

                await Clients.Caller.ReceivedMessage(a);
                await Task.Delay(new TimeSpan(0, 0, 5));
            }
        }

        public override async Task OnConnectedAsync()
        {            
            //await Clients.Caller.ReceivedMessage($"You are connected to Booking chanel! ConnectionId: {Context.ConnectionId}");
            //await GetAllBookings();
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

    }

}

