using bookingtaxi_backend.Model;

namespace bookingtaxi_backend.Hub
{
    public interface ITypeHub
    {
        Task ReceivedMessage(string username, string message);
        Task ReceivedMessage(string message);
        Task ReceivedMessage(List<Booking> message);
    }
}
