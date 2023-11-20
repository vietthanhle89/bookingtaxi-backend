using bookingtaxi_backend.Model;

namespace bookingtaxi_backend.Hub
{
    public interface ITypeHub
    {
        Task ReceivedMessage(string username, string message, string time);
        Task ReceivedMessage(string username, string message);
        Task ReceivedMessage(string message);
        Task ReceivedMessage(List<Booking> message);
        Task ReceivedMessage(Booking? message);
        Task ReceivedMessage(Driver? driver);
        Task ReceivedMessage(DriverCar? driverCar);
    }
}
